using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblyReflection.Extensions;
using Core.Constants;
using Core.Model;

namespace AssemblyReflection.ReflectorLoader
{
    public partial class Reflector
    {
        internal MethodMetadata LoadMethodMetadataDto(MethodBase method, AssemblyMetadataStore metaStore)
        {
            if (method == null)
            {
                throw new ArgumentNullException($"{nameof(method)} argument is null.");
            }

            MethodMetadata methodMetadataDto = new MethodMetadata()
            {
                Name = method.Name,
                Modifiers = EmitModifiers(method),
                Extension = IsExtension(method)
            };

            methodMetadataDto.GenericArguments = !method.IsGenericMethodDefinition ? new List<TypeMetadata>() : EmitGenericArguments(method.GetGenericArguments(), metaStore);
            methodMetadataDto.ReturnType = EmitReturnType(method, metaStore);
            methodMetadataDto.Parameters = EmitParameters(method.GetParameters(), metaStore).ToList();

            string parameters = methodMetadataDto.Parameters.Any()
                ? methodMetadataDto.Parameters.Select(methodInstance => methodInstance.Name)
                    .Aggregate((current, next) => current + ", " + next)
                : "none";

            string generics = methodMetadataDto.GenericArguments.Any()
                ? methodMetadataDto.GenericArguments.Select(typeInstance => typeInstance.Id)
                    .Aggregate((c, n) => $"{c}, {n}")
                : "none";

            methodMetadataDto.Id = $"{method.DeclaringType.FullName}{method.Name} args {parameters} generics {generics} declaredBy {method.DeclaringType.FullName}";

            if (!metaStore.MethodsDictionary.ContainsKey(methodMetadataDto.Id))
            {
                _logger.Trace("Adding method to dictionary: Id =" + methodMetadataDto.Id);
                metaStore.MethodsDictionary.Add(methodMetadataDto.Id, methodMetadataDto);
                return methodMetadataDto;
            }
            else
            {
                _logger.Trace("Using method already added to dictionary: Id =" + methodMetadataDto.Id);
                return metaStore.MethodsDictionary[methodMetadataDto.Id];
            }
        }

        internal IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods, AssemblyMetadataStore metaStore)
        {
            return (from MethodBase currentMethod in methods
                    where currentMethod.IsVisible()
                    select LoadMethodMetadataDto(currentMethod, metaStore)).ToList();
        }

        private IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parameters, AssemblyMetadataStore metaStore)
        {
            List<ParameterMetadata> parametersMetadata = new List<ParameterMetadata>();
            foreach (var parameter in parameters)
            {
                string id = $"{parameter.ParameterType.FullName}.{parameter.Name}";
                if (metaStore.ParametersDictionary.ContainsKey(id))
                {
                    _logger.Trace("Using parameter already added to dictionary: Id =" + id);
                    parametersMetadata.Add(metaStore.ParametersDictionary[id]);
                }
                else
                {
                    ParameterMetadata newParameter = new ParameterMetadata(parameter.Name, LoadTypeMetadataDto(parameter.ParameterType, metaStore));
                    newParameter.Id = id;
                    metaStore.ParametersDictionary.Add(id, newParameter);
                    _logger.Trace("Adding parameter to dictionary: Id =" + id);
                    parametersMetadata.Add(newParameter);
                }
            }

            return parametersMetadata;
        }

        private TypeMetadata EmitReturnType(MethodBase method, AssemblyMetadataStore metaStore)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return methodInfo == null ? null : LoadTypeMetadataDto(methodInfo.ReturnType, metaStore);
        }

        private static bool IsExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> EmitModifiers(MethodBase method)
        {
            Accessibility access = Accessibility.Private;
            if (method.IsPublic)
            {
                access = Accessibility.Public;
            }
            else if (method.IsFamily)
            {
                access = Accessibility.Protected;
            }
            else if (method.IsFamilyAndAssembly)
            {
                access = Accessibility.ProtectedInternal;
            }

            IsAbstract isAbstract = IsAbstract.NotAbstract;
            if (method.IsAbstract)
            {
                isAbstract = IsAbstract.Abstract;
            }

            IsStatic isStatic = IsStatic.NotStatic;
            if (method.IsStatic)
            {
                isStatic = IsStatic.Static;
            }

            IsVirtual isVirtual = IsVirtual.NotVirtual;
            if (method.IsVirtual)
            {
                isVirtual = IsVirtual.Virtual;
            }

            return new Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual>(access, isAbstract, isStatic, isVirtual);
        }
    }
}
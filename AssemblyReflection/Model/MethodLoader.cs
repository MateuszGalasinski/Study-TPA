using AssemblyReflection.ExtensionMethods;
using Core.Constants;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyReflection.Model
{
    internal class MethodLoader
    {
        internal static MethodMetadata LoadMethodMetadata(MethodBase method, AssemblyMetadataStore metaStore)
        {
            MethodMetadata methodMetadata = new MethodMetadata()
            {
                Name = method.Name,
                Modifiers = EmitModifiers(method),
                Extension = EmitExtension(method)
            };
            methodMetadata.GenericArguments = !method.IsGenericMethodDefinition ? null : TypeLoader.EmitGenericArguments(method.GetGenericArguments());
            methodMetadata.ReturnType = EmitReturnType(method);
            methodMetadata.Parameters = EmitParameters(method.GetParameters());

            return methodMetadata;
        }

        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods, AssemblyMetadataStore metaStore)
        {
            return from MethodBase currentMethod in methods
                where currentMethod.IsVisible()
                select LoadMethodMetadata(currentMethod, metaStore);
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parameters)
        {
            return from parameter in parameters
                   select new ParameterMetadata(parameter.Name, TypeLoader.EmitReference(parameter.ParameterType));
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return methodInfo == null ? null : TypeLoader.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> EmitModifiers(MethodBase method)
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
using Core.Constants;
using Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Core.Model
{
    internal class MethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                   where currentMethod.IsVisible()
                   select new MethodMetadata(currentMethod);
        }

        private string _name;
        private IEnumerable<TypeMetadata> _genericArguments;
        private Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> m_Modifiers;
        private TypeMetadata _returnType;
        private bool _extension;
        private IEnumerable<ParameterMetadata> _parameters;

        private MethodMetadata(MethodBase method)
        {
            _name = method.Name;
            _genericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            _returnType = EmitReturnType(method);
            _parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            _extension = EmitExtension(method);
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
            {
                return null;
            }

            return TypeMetadata.EmitReference(methodInfo.ReturnType);
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
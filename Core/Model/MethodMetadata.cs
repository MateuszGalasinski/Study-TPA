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
            return from MethodBase _currentMethod in methods
                   where _currentMethod.IsVisible()
                   select new MethodMetadata(_currentMethod);
        }

        private string m_Name;
        private IEnumerable<TypeMetadata> m_GenericArguments;
        private Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> m_Modifiers;
        private TypeMetadata m_ReturnType;
        private bool m_Extension;
        private IEnumerable<ParameterMetadata> m_Parameters;

        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
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
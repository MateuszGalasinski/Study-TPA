using Core.Constants;
using Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionLoading.Models
{

    public class MethodModel
    {
        public string Name { get; set; }
        
        public List<TypeModel> GenericArguments { get; set; }

        public Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> Modifiers { get; set; }

        
        public TypeModel ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterModel> Parameters { get; set; }

        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        private List<TypeModel> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new TypeModel(t)).ToList();
        }

        public static List<MethodModel> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodModel(t)).ToList();
        }

        private static List<ParameterModel> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterModel(t.Name,TypeModel.EmitReference(t.ParameterType))).ToList( );
        }

        private static TypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeModel.StoreType(methodInfo.ReturnType);
            return TypeModel.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual> EmitModifiers(MethodBase method)
        {
            Accessibility access = method.IsPublic ? Accessibility.Public :
                method.IsFamily ? Accessibility.Protected :
                method.IsAssembly ? Accessibility.ProtectedInternal : Accessibility.Private;

            IsAbstract _abstract = method.IsAbstract ? IsAbstract.Abstract : IsAbstract.NotAbstract;

            IsStatic _static = method.IsStatic ? IsStatic.Static : IsStatic.NotStatic;

            IsVirtual _virtual = method.IsVirtual ? IsVirtual.Virtual : IsVirtual.NotVirtual;

            return new Tuple<Accessibility, IsAbstract, IsStatic, IsVirtual>(access, _abstract, _static, _virtual);
        }

        public static List<MethodModel> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodModel(t)).ToList();
        }
    }
}

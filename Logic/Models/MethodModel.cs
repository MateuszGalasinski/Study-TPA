using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Logic.Enums;

namespace Logic.Models
{
    public class MethodModel
    {
        public string Name { get; set; }

        public List<TypeModel> GenericArguments { get; set; }

        public Accessibility Accessibility { get; set; }

        public IsAbstract IsAbstract { get; set; }

        public IsStatic IsStatic { get; set; }

        public IsVirtual IsVirtual { get; set; }

        public TypeModel ReturnType { get; set; }

        public bool Extension { get; set; }

        public List<ParameterModel> Parameters { get; set; }

        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? new List<TypeModel>() : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodModel(BaseCore.Model.MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.IsAbstract = baseMethod.IsAbstract.ToLogicEnum();
            this.Accessibility = baseMethod.Accessibility.ToLogicEnum();
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeModel.GetOrAdd(baseMethod.ReturnType);
            this.IsStatic = baseMethod.IsStatic.ToLogicEnum();
            this.IsVirtual = baseMethod.VirtualEnum.ToLogicEnum();

            GenericArguments = baseMethod.GenericArguments?.Select(TypeModel.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterModel(t)).ToList();

        }

        private List<TypeModel> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => TypeModel.GetOrAdd(t)).ToList();
        }

        public static List<MethodModel> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodModel(t)).ToList();
        }

        private static List<ParameterModel> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterModel(t.Name, TypeModel.GetOrAdd(t.ParameterType))).ToList();
        }

        private static TypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeModel.StoreType(methodInfo.ReturnType);
            return TypeModel.GetOrAdd(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private void EmitModifiers(MethodBase method)
        {
            Accessibility = method.IsPublic ? Accessibility.IsPublic :
                method.IsFamily ? Accessibility.IsProtected :
                method.IsAssembly ? Accessibility.Internal : Accessibility.IsPrivate;

            IsAbstract = method.IsAbstract ? IsAbstract.Abstract : IsAbstract.NotAbstract;

            IsStatic = method.IsStatic ? IsStatic.Static : IsStatic.NotStatic;

            IsVirtual = method.IsVirtual ? IsVirtual.Virtual : IsVirtual.NotVirtual;
        }

        public static List<MethodModel> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodModel(t)).ToList();
        }
    }
}
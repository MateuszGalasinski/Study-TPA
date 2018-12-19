using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ReflectionLoading.Models
{
    [DataContract(Name = "MethodModel")]
    public class MethodModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }
        [DataMember]
        public Accessibility Accessibility { get; set; }
        [DataMember]
        public IsSealed IsSealed { get; set; }
        [DataMember]
        public IsAbstract IsAbstract { get; set; }
        [DataMember]
        public IsStatic IsStatic { get; set; }
        [DataMember]
        public IsVirtual IsVirtual { get; set; }
        [DataMember]
        public TypeModel ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public List<ParameterModel> Parameters { get; set; }

        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            LoadModifiers(method);
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
        private void LoadModifiers(MethodBase method)
        {
            Accessibility = method.IsPublic ? Accessibility.Public :
                method.IsFamily ? Accessibility.Protected :
                method.IsAssembly ? Accessibility.ProtectedInternal : Accessibility.Private;

            IsAbstract = method.IsAbstract ? IsAbstract.Abstract : IsAbstract.NotAbstract;

            IsStatic = method.IsStatic ? IsStatic.Static : IsStatic.NotStatic;

            IsVirtual = method.IsVirtual ? IsVirtual.Virtual : IsVirtual.NotVirtual;
        }

        public static List<MethodModel> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodModel(t)).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as MethodModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<TypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   IsVirtual == model.IsVirtual &&
                   EqualityComparer<TypeModel>.Default.Equals(ReturnType, model.ReturnType) &&
                   Extension == model.Extension &&
                   EqualityComparer<List<ParameterModel>>.Default.Equals(Parameters, model.Parameters);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.Constants;

namespace ReflectionLoading.LogicModels
{
    public class LogicMethodModel
    {
        public string Name { get; set; }
        
        public List<LogicTypeModel> GenericArguments { get; set; }
        
        public Accessibility Accessibility { get; set; }
        
        public IsSealed IsSealed { get; set; }
        
        public IsAbstract IsAbstract { get; set; }
        
        public IsStatic IsStatic { get; set; }
        
        public IsVirtual IsVirtual { get; set; }
        
        public LogicTypeModel ReturnType { get; set; }
        
        public bool Extension { get; set; }
        
        public List<LogicParameterModel> Parameters { get; set; }

        public LogicMethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            LoadModifiers(method);
            Extension = EmitExtension(method);
        }

        private List<LogicTypeModel> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new LogicTypeModel(t)).ToList();
        }
        public static List<LogicMethodModel> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new LogicMethodModel(t)).ToList();
        }
        private static List<LogicParameterModel> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new LogicParameterModel(t.Name,LogicTypeModel.EmitReference(t.ParameterType))).ToList( );
        }
        private static LogicTypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            LogicTypeModel.StoreType(methodInfo.ReturnType);
            return LogicTypeModel.EmitReference(methodInfo.ReturnType);
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

        public static List<LogicMethodModel> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new LogicMethodModel(t)).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as LogicMethodModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<LogicTypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   IsVirtual == model.IsVirtual &&
                   EqualityComparer<LogicTypeModel>.Default.Equals(ReturnType, model.ReturnType) &&
                   Extension == model.Extension &&
                   EqualityComparer<List<LogicParameterModel>>.Default.Equals(Parameters, model.Parameters);
        }
    }
}

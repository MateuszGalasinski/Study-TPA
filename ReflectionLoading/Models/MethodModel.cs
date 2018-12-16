using Core.Constants;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.Model;

namespace ReflectionLoading.Models
{
    public class MethodModel : BaseMethodModel
    {
        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            LoadModifiers(method);
            Extension = EmitExtension(method);
        }

        private List<BaseTypeModel> EmitGenericArguments(MethodBase method)
        {
            List<BaseTypeModel> typeModels = new List<BaseTypeModel>();
            foreach (var genericArgument in method.GetGenericArguments())
            {
                typeModels.Add(new TypeModel(genericArgument));
            }

            return typeModels;
            //return method.GetGenericArguments().Select(t => new TypeModel(t)).ToList();
        }
        public static List<BaseMethodModel> EmitMethods(Type type)
        {
            List<BaseMethodModel> methodModels = new List<BaseMethodModel>();
            foreach (var methodInfo in type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                                     BindingFlags.Static | BindingFlags.Instance))
            {
                methodModels.Add(new MethodModel(methodInfo));
            }

            return methodModels;
            //return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
            //                       BindingFlags.Static | BindingFlags.Instance).Select(t => new BaseMethodModel(t)).ToList();
        }
        private static List<BaseParameterModel> EmitParameters(MethodBase method)
        {
            List<BaseParameterModel> parameterModels = new List<BaseParameterModel>();
            foreach (var parameterInfo in method.GetParameters())
            {
                parameterModels.Add(new ParameterModel(parameterInfo.Name, TypeModel.EmitReference(parameterInfo.ParameterType)));
            }
            return parameterModels;
            //return method.GetParameters().Select(t => new ParameterModel(t.Name, TypeModel.EmitReference(t.ParameterType))).ToList();
        }
        private static BaseTypeModel EmitReturnType(MethodBase method)
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

        public static List<BaseMethodModel> EmitConstructors(Type type)
        {
            List<BaseMethodModel> constuctorMethodModels = new List<BaseMethodModel>();
            foreach (var constructorInfo in type.GetConstructors())
            {
                constuctorMethodModels.Add(new MethodModel(constructorInfo));
            }
            return constuctorMethodModels;
            //return type.GetConstructors().Select(t => new MethodModel(t)).ToList();
        }

        public override bool Equals(object obj)
        {
            var model = obj as MethodModel;
            return model != null &&
                   Name == model.Name &&
                   EqualityComparer<List<BaseTypeModel>>.Default.Equals(GenericArguments, model.GenericArguments) &&
                   Accessibility == model.Accessibility &&
                   IsSealed == model.IsSealed &&
                   IsAbstract == model.IsAbstract &&
                   IsStatic == model.IsStatic &&
                   IsVirtual == model.IsVirtual &&
                   EqualityComparer<BaseTypeModel>.Default.Equals(ReturnType, model.ReturnType) &&
                   Extension == model.Extension &&
                   EqualityComparer<List<BaseParameterModel>>.Default.Equals(Parameters, model.Parameters);
        }
    }
}

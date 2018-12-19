using BaseCore.Enums;
using Logic.Enums;
using System;
using System.Reflection;
using IsAbstract = BaseCore.Enums.IsAbstract;
using IsSealed = BaseCore.Enums.IsSealed;
using IsStatic = BaseCore.Enums.IsStatic;
using IsVirtual = BaseCore.Enums.IsVirtual;
using TypeKind = BaseCore.Enums.TypeKind;

namespace Logic.Models
{
    public static class ExtensionMethods
    {
        internal static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        internal static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns ?? string.Empty;
        }

        internal static Enums.IsAbstract ToLogicEnum(this IsAbstract baseEnum)
        {
            switch (baseEnum)
            {
                case IsAbstract.Abstract:
                    return Enums.IsAbstract.Abstract;

                case IsAbstract.NotAbstract:
                    return Enums.IsAbstract.NotAbstract;
                default:
                     throw new Exception();
            }
        }

        internal static Accessibility ToLogicEnum(this AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case AccessLevel.Default:
                    return Accessibility.Default;

                case AccessLevel.Internal:
                    return Accessibility.Internal;

                case AccessLevel.IsPrivate:
                    return Accessibility.IsPrivate;

                case AccessLevel.IsProtected:
                    return Accessibility.IsProtected;

                case AccessLevel.IsProtectedInternal:
                    return Accessibility.IsProtectedInternal;

                case AccessLevel.IsPublic:
                    return Accessibility.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsSealed ToLogicEnum(this IsSealed baseEnum)
        {
            switch (baseEnum)
            {
                case IsSealed.NotSealed:
                    return Enums.IsSealed.NotSealed;

                case IsSealed.Sealed:
                    return Enums.IsSealed.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsStatic ToLogicEnum(this IsStatic baseEnum)
        {
            switch (baseEnum)
            {
                case IsStatic.Static:
                    return Enums.IsStatic.Static;

                case IsStatic.NotStatic:
                    return Enums.IsStatic.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.TypeKind ToLogicEnum(this TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case TypeKind.ClassType:
                    return Enums.TypeKind.ClassType;
                case TypeKind.EnumType:
                    return Enums.TypeKind.EnumType;
                case TypeKind.InterfaceType:
                    return Enums.TypeKind.InterfaceType;
                case TypeKind.StructType:
                    return Enums.TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static Enums.IsVirtual ToLogicEnum(this IsVirtual baseEnum)
        {
            switch (baseEnum)
            {
                case IsVirtual.NotVirtual:
                    return Enums.IsVirtual.NotVirtual;

                case IsVirtual.Virtual:
                    return Enums.IsVirtual.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

using Logic.Enums;
using System;
using System.Reflection;

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

        internal static IsAbstract ToLogicEnum(this Base.Enums.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.AbstractEnum.Abstract:
                    return IsAbstract.Abstract;

                case Base.Enums.AbstractEnum.NotAbstract:
                    return IsAbstract.NotAbstract;
                default:
                     throw new Exception();
            }
        }

        internal static Accessibility ToLogicEnum(this Base.Enums.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.AccessLevel.Default:
                    return Accessibility.Default;

                case Base.Enums.AccessLevel.Internal:
                    return Accessibility.Internal;

                case Base.Enums.AccessLevel.IsPrivate:
                    return Accessibility.IsPrivate;

                case Base.Enums.AccessLevel.IsProtected:
                    return Accessibility.IsProtected;

                case Base.Enums.AccessLevel.IsProtectedInternal:
                    return Accessibility.IsProtectedInternal;

                case Base.Enums.AccessLevel.IsPublic:
                    return Accessibility.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static IsSealed ToLogicEnum(this Base.Enums.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.SealedEnum.NotSealed:
                    return IsSealed.NotSealed;

                case Base.Enums.SealedEnum.Sealed:
                    return IsSealed.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static IsStatic ToLogicEnum(this Base.Enums.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.StaticEnum.Static:
                    return IsStatic.Static;

                case Base.Enums.StaticEnum.NotStatic:
                    return IsStatic.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static TypeKind ToLogicEnum(this Base.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.TypeKind.ClassType:
                    return TypeKind.ClassType;
                case Base.Enums.TypeKind.EnumType:
                    return TypeKind.EnumType;
                case Base.Enums.TypeKind.InterfaceType:
                    return TypeKind.InterfaceType;
                case Base.Enums.TypeKind.StructType:
                    return TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static IsVirtual ToLogicEnum(this Base.Enums.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Base.Enums.VirtualEnum.NotVirtual:
                    return IsVirtual.NotVirtual;

                case Base.Enums.VirtualEnum.Virtual:
                    return IsVirtual.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

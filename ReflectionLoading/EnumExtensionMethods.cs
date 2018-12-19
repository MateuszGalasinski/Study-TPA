using BaseCore.Enums;
using System;

namespace ReflectionLoading
{
    public static class EnumExtensionMethods
    {
        internal static IsAbstract ToBaseEnum(this Logic.Enums.IsAbstract baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsAbstract.Abstract:
                    return IsAbstract.Abstract;
              
                case Logic.Enums.IsAbstract.NotAbstract:
                    return IsAbstract.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static AccessLevel ToBaseEnum(this Logic.Enums.Accessibility baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.Accessibility.Default:
                    return AccessLevel.Default;

                case Logic.Enums.Accessibility.Internal:
                    return AccessLevel.Internal;

                case Logic.Enums.Accessibility.IsPrivate:
                    return AccessLevel.IsPrivate;

                case Logic.Enums.Accessibility.IsProtected:
                    return AccessLevel.IsProtected;

                case Logic.Enums.Accessibility.IsProtectedInternal:
                    return AccessLevel.IsProtectedInternal;

                case Logic.Enums.Accessibility.IsPublic:
                    return AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static IsSealed ToBaseEnum(this Logic.Enums.IsSealed baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsSealed.NotSealed:
                    return IsSealed.NotSealed;

                case Logic.Enums.IsSealed.Sealed:
                    return IsSealed.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static IsStatic ToBaseEnum(this Logic.Enums.IsStatic baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsStatic.Static:
                    return IsStatic.Static;

                case Logic.Enums.IsStatic.NotStatic:
                    return IsStatic.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static TypeKind ToBaseEnum(this Logic.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.TypeKind.ClassType:
                    return TypeKind.ClassType;
                case Logic.Enums.TypeKind.EnumType:
                    return TypeKind.EnumType;
                case Logic.Enums.TypeKind.InterfaceType:
                    return TypeKind.InterfaceType;
                case Logic.Enums.TypeKind.StructType:
                    return TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static IsVirtual ToBaseEnum(this Logic.Enums.IsVirtual baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsVirtual.NotVirtual:
                    return IsVirtual.NotVirtual;

                case Logic.Enums.IsVirtual.Virtual:
                    return IsVirtual.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

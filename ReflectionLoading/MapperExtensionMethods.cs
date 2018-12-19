using System;

namespace ReflectionLoading
{
    public static class MapperExtensionMethods
    {
        internal static Base.Enums.AbstractEnum ToBaseEnum(this Logic.Enums.IsAbstract baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsAbstract.Abstract:
                    return Base.Enums.AbstractEnum.Abstract;
              
                case Logic.Enums.IsAbstract.NotAbstract:
                    return Base.Enums.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static Base.Enums.AccessLevel ToBaseEnum(this Logic.Enums.Accessibility baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.Accessibility.Default:
                    return Base.Enums.AccessLevel.Default;

                case Logic.Enums.Accessibility.Internal:
                    return Base.Enums.AccessLevel.Internal;

                case Logic.Enums.Accessibility.IsPrivate:
                    return Base.Enums.AccessLevel.IsPrivate;

                case Logic.Enums.Accessibility.IsProtected:
                    return Base.Enums.AccessLevel.IsProtected;

                case Logic.Enums.Accessibility.IsProtectedInternal:
                    return Base.Enums.AccessLevel.IsProtectedInternal;

                case Logic.Enums.Accessibility.IsPublic:
                    return Base.Enums.AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static Base.Enums.SealedEnum ToBaseEnum(this Logic.Enums.IsSealed baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsSealed.NotSealed:
                    return Base.Enums.SealedEnum.NotSealed;

                case Logic.Enums.IsSealed.Sealed:
                    return Base.Enums.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Base.Enums.StaticEnum ToBaseEnum(this Logic.Enums.IsStatic baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsStatic.Static:
                    return Base.Enums.StaticEnum.Static;

                case Logic.Enums.IsStatic.NotStatic:
                    return Base.Enums.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static Base.Enums.TypeKind ToBaseEnum(this Logic.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.TypeKind.ClassType:
                    return Base.Enums.TypeKind.ClassType;
                case Logic.Enums.TypeKind.EnumType:
                    return Base.Enums.TypeKind.EnumType;
                case Logic.Enums.TypeKind.InterfaceType:
                    return Base.Enums.TypeKind.InterfaceType;
                case Logic.Enums.TypeKind.StructType:
                    return Base.Enums.TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }
        internal static Base.Enums.VirtualEnum ToBaseEnum(this Logic.Enums.IsVirtual baseEnum)
        {
            switch (baseEnum)
            {
                case Logic.Enums.IsVirtual.NotVirtual:
                    return Base.Enums.VirtualEnum.NotVirtual;

                case Logic.Enums.IsVirtual.Virtual:
                    return Base.Enums.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

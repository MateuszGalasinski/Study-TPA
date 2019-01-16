using System;
using System.Reflection;
using Accessibility = Logic.Enums.Accessibility;

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

        internal static BaseCore.Enums.IsAbstract ToLogicEnum(this Enums.IsAbstract baseEnum)
        {
            switch (baseEnum)
            {
                case Enums.IsAbstract.Abstract:
                    return BaseCore.Enums.IsAbstract.Abstract;

                case Enums.IsAbstract.NotAbstract:
                    return BaseCore.Enums.IsAbstract.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static BaseCore.Enums.Accessibility ToLogicEnum(this Enums.Accessibility baseEnum)
        {
            switch (baseEnum)
            {
                case Accessibility.Default:
                    return BaseCore.Enums.Accessibility.Default;

                case Accessibility.Internal:
                    return BaseCore.Enums.Accessibility.Internal;

                case Accessibility.IsPrivate:
                    return BaseCore.Enums.Accessibility.IsPrivate;

                case Accessibility.IsProtected:
                    return BaseCore.Enums.Accessibility.IsProtected;

                case Accessibility.IsProtectedInternal:
                    return BaseCore.Enums.Accessibility.IsProtectedInternal;

                case Accessibility.IsPublic:
                    return BaseCore.Enums.Accessibility.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static BaseCore.Enums.IsSealed ToLogicEnum(this Enums.IsSealed baseEnum)
        {
            switch (baseEnum)
            {
                case Enums.IsSealed.NotSealed:
                    return BaseCore.Enums.IsSealed.NotSealed;

                case Enums.IsSealed.Sealed:
                    return BaseCore.Enums.IsSealed.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static BaseCore.Enums.IsStatic ToLogicEnum(this Enums.IsStatic baseEnum)
        {
            switch (baseEnum)
            {
                case Enums.IsStatic.Static:
                    return BaseCore.Enums.IsStatic.Static;

                case Enums.IsStatic.NotStatic:
                    return BaseCore.Enums.IsStatic.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static BaseCore.Enums.TypeKind ToLogicEnum(this Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Enums.TypeKind.ClassType:
                    return BaseCore.Enums.TypeKind.ClassType;
                case Enums.TypeKind.EnumType:
                    return BaseCore.Enums.TypeKind.EnumType;
                case Enums.TypeKind.InterfaceType:
                    return BaseCore.Enums.TypeKind.InterfaceType;
                case Enums.TypeKind.StructType:
                    return BaseCore.Enums.TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }

        internal static BaseCore.Enums.IsVirtual ToLogicEnum(this Enums.IsVirtual baseEnum)
        {
            switch (baseEnum)
            {
                case Enums.IsVirtual.NotVirtual:
                    return BaseCore.Enums.IsVirtual.NotVirtual;

                case Enums.IsVirtual.Virtual:
                    return BaseCore.Enums.IsVirtual.Virtual;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsAbstract ToLogicEnum(this BaseCore.Enums.IsAbstract baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.IsAbstract.Abstract:
                    return Enums.IsAbstract.Abstract;

                case BaseCore.Enums.IsAbstract.NotAbstract:
                    return Enums.IsAbstract.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.Accessibility ToLogicEnum(this BaseCore.Enums.Accessibility baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.Accessibility.Default:
                    return Enums.Accessibility.Default;

                case BaseCore.Enums.Accessibility.Internal:
                    return Enums.Accessibility.Internal;

                case BaseCore.Enums.Accessibility.IsPrivate:
                    return Enums.Accessibility.IsPrivate;

                case BaseCore.Enums.Accessibility.IsProtected:
                    return Enums.Accessibility.IsProtected;

                case BaseCore.Enums.Accessibility.IsProtectedInternal:
                    return Enums.Accessibility.IsProtectedInternal;

                case BaseCore.Enums.Accessibility.IsPublic:
                    return Enums.Accessibility.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsSealed ToLogicEnum(this BaseCore.Enums.IsSealed baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.IsSealed.NotSealed:
                    return Enums.IsSealed.NotSealed;

                case BaseCore.Enums.IsSealed.Sealed:
                    return Enums.IsSealed.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsStatic ToLogicEnum(this BaseCore.Enums.IsStatic baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.IsStatic.Static:
                    return Enums.IsStatic.Static;

                case BaseCore.Enums.IsStatic.NotStatic:
                    return Enums.IsStatic.NotStatic;
                default:
                    throw new Exception();
            }
        }

        internal static Enums.TypeKind ToLogicEnum(this BaseCore.Enums.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.TypeKind.ClassType:
                    return Enums.TypeKind.ClassType;
                case BaseCore.Enums.TypeKind.EnumType:
                    return Enums.TypeKind.EnumType;
                case BaseCore.Enums.TypeKind.InterfaceType:
                    return Enums.TypeKind.InterfaceType;
                case BaseCore.Enums.TypeKind.StructType:
                    return Enums.TypeKind.StructType;

                default:
                    throw new Exception();
            }
        }

        internal static Enums.IsVirtual ToLogicEnum(this BaseCore.Enums.IsVirtual baseEnum)
        {
            switch (baseEnum)
            {
                case BaseCore.Enums.IsVirtual.NotVirtual:
                    return Enums.IsVirtual.NotVirtual;

                case BaseCore.Enums.IsVirtual.Virtual:
                    return Enums.IsVirtual.Virtual;
                default:
                    throw new Exception();
            }
        }
    }
}

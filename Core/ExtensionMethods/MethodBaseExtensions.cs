using System.Reflection;

namespace Core.ExtensionMethods
{
    internal static class MethodBaseExtensions
    {
        internal static bool IsVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }
    }
}

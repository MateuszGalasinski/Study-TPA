using System;
using System.Collections.Generic;
using UILogic.Model;

namespace TreeViewConsoleApp
{
    public static class TreeItemToStringConverter
    {
        public static readonly Dictionary<Type, string> Map = new Dictionary<Type, string>()
        {
            { typeof(TypeTreeItem), "Type" },
            { typeof(PropertyTreeItem), "Property" },
            { typeof(ParameterTreeItem), "Parameter" },
            { typeof(NamespaceTreeItem), "Namespace" },
            { typeof(MethodTreeItem), "Method" },
            { typeof(AssemblyTreeItem), "Assembly" },
        };

        public static string GetNameFromType(object value)
        {
            if (value != null && Map.TryGetValue(value.GetType(), out string name))
            {
                return $"{name}";
            }

            return "Unknown";
        }
    }
}

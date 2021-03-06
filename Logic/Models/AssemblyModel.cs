﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BaseCore.Model;

namespace Logic.Models
{
    public class AssemblyModel
    {
        public List<NamespaceModel> NamespaceModels { get; set; }

        public string Name { get; set; }

        public static string CurrentAssemblyName;

        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            CurrentAssemblyName = assembly.ManifestModule.FullyQualifiedName;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList())).ToList();
        }

        public AssemblyModel(AssemblyBase assemblybase)
        {
            this.Name = assemblybase.Name;
            this.NamespaceModels = assemblybase.Namespaces?.Select(ns => new NamespaceModel(ns)).ToList();
        }


    }
}

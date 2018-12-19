using ReflectionLoading.Exceptions;
using ReflectionLoading.Models;
using System;
using System.IO;
using System.Reflection;
using Base.Model;
using Core.Components;

namespace ReflectionLoading
{
    public class AssemblyManager
    {
        public AssemblyModel AssemblyModel { get; private set; }

        private ISerializator<AssemblyBase>

        public void LoadAssembly(string assemblyPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assemblyPath))
                    throw new System.ArgumentNullException();
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                TypeModel.TypeDictionary.Clear();
                AssemblyModel = new AssemblyModel(assembly);
            }
            catch (FileLoadException e)
            {
                throw new ArgumentException("Could not load this file", e);
            }
            catch (Exception e)
            {
                throw new ReflectionLoadException("Error occured during assembly loading.", e);
            }
        }

        public void LoadAssembly(AssemblyModel assemblyModel)
        {
            try
            {
                if (assemblyModel == null)
                {
                    throw new ArgumentNullException("Deserialized AssemblyModel is null");
                }
                TypeModel.TypeDictionary.Clear();
                assemblyModel.NamespaceModels.ForEach(n => n.Types.ForEach(t => TypeModel.TypeDictionary.Add(t.Name, t)));
                AssemblyModel = assemblyModel;
            }
            catch (Exception e)
            {
                throw new ReflectionLoadException("Could not deserialize", e);
            }
        }

        public void SaveAssembly()
        {

        }

        public void LoadAssemblyFromStorage()
        {

        }
    }
}

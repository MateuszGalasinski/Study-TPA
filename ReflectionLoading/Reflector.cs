using ReflectionLoading.Exceptions;
using ReflectionLoading.Models;
using System;
using System.IO;
using System.Reflection;

namespace ReflectionLoading
{
    public class Reflector
    {
        public AssemblyModel AssemblyModel { get; private set; }

        public Reflector(string assemblyPath)
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
        public Reflector(AssemblyModel assemblyModel)
        {
            try
            {
                if (assemblyModel == null)
                {
                    throw new System.ArgumentNullException("Deserialized AssemblyModel is null");
                }
                TypeModel.TypeDictionary.Clear();
                AssemblyModel = assemblyModel;
            }
            catch (Exception e)
            {
                throw new ReflectionLoadException("Could not deserialize", e);
            }
        }
    }
}

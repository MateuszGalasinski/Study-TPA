using Autofac;
using Core.Model;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core
{
    public static class ContainerProvider
    {
        public static IContainer BuildContainer(string assembliesFolder)
        {
            var builder = new ContainerBuilder();
            var assembliesEnumerator = Directory.EnumerateFiles(assembliesFolder);

            foreach (var fileName in assembliesEnumerator.Where(fileName => fileName.EndsWith(".dll")))
            {
                builder.RegisterAssemblyModules<BaseRegisterModule>(Assembly.LoadFrom(fileName));
            }

            return builder.Build();
        }
    }
}

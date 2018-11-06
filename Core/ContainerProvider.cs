using Autofac;
using Core.Model;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core
{
    public static class ContainerProvider
    {
        private static readonly string[] AssemblyFormats = new[] { ".dll", ".exe" };
        public static IContainer BuildContainer(string assembliesFolder)
        {
            var builder = new ContainerBuilder();
            var assembliesEnumerator = Directory.EnumerateFiles(assembliesFolder);

            foreach (var fileName in assembliesEnumerator.Where(fileName => AssemblyFormats.Any(fileName.EndsWith)))
            {
                builder.RegisterAssemblyModules<BaseRegisterModule>(Assembly.LoadFrom(fileName));
            }

            return builder.Build();
        }
    }
}

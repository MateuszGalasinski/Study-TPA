using Autofac;
using Core.Components;
using Core.Model;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core
{
    public class ContainerProvider
    {
        [Import(typeof(ICompositionConfiguration))]
        public ICompositionConfiguration CompositionConfiguration;

        private readonly string[] _assemblyFormats = new[] { ".dll", ".exe" };

        public IContainer BuildContainer(string assembliesFolder)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(CompositionConfiguration)
                .As<ICompositionConfiguration>()
                .SingleInstance();

            var assembliesEnumerator = Directory.EnumerateFiles(assembliesFolder);

            foreach (var fileName in assembliesEnumerator.Where(fileName => _assemblyFormats.Any(fileName.EndsWith)))
            {
                builder.RegisterAssemblyModules<BaseRegisterModule>(Assembly.LoadFrom(fileName));
            }

            return builder.Build();
        }
    }
}

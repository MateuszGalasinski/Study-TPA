using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using ReflectionLoading;
using UILogic.Interfaces;
using UILogic.ViewModel;

namespace UILogic.Services
{
    public static class Composer
    {
        public static MainViewModel GetComposedMainViewModel(
            IFilePathGetter filePathGetter)
        {
            MainViewModel mainViewModel = new MainViewModel(filePathGetter, new AssemblyManager());

            //in order to force exception
            List<AssemblyCatalog> catalogs = new List<AssemblyCatalog>();
            List<ComposablePartDefinition[]> parts = new List<ComposablePartDefinition[]>();
            string path = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                AssemblyCatalog assemblyCatalog = new AssemblyCatalog(Assembly.LoadFile(dll));
                parts.Add(assemblyCatalog.Parts.ToArray());
            }

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(mainViewModel);
                mainViewModel.AssemblyManager = AssemblyManager.GetComposed();
            }
            catch (CompositionException compositionException)
            {
                throw new ApplicationException("Could not compose application successfully. ", compositionException);
            }

            return mainViewModel;
        }
    }
}

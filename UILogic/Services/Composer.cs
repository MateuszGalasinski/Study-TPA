using ReflectionLoading;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../Logging/bin/Debug"));
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

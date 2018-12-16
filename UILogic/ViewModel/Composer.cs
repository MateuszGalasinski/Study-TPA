using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using UILogic.Interfaces;

namespace UILogic.ViewModel
{
    public static class Composer
    {
        public static MainViewModel GetComposedMainViewModel(IFilePathGetter filePathGetter)
        {
            MainViewModel mainViewModel = new MainViewModel(filePathGetter);
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../XmlSerialization/bin/Debug"));
            catalog.Catalogs.Add(new DirectoryCatalog("../../../Logging/bin/Debug"));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(mainViewModel);

            return mainViewModel;
        }
    }
}
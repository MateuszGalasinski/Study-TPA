using Autofac;
using Core;
using SharedUILogic.ViewModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace SharedUILogic
{
    public class Bootstraper
    {
        private static ILifetimeScope _rootScope;

        public static IMainViewModel MainViewModel
        {
            get
            {
                if (_rootScope == null)
                {
                    Initialize();
                }

                return _rootScope.Resolve<IMainViewModel>();
            }
        }

        private static void Initialize()
        {
            if (_rootScope != null)
            {
                return;
            }

            _rootScope = ComposeContainerProvider().BuildContainer("./");
        }

        private static ContainerProvider ComposeContainerProvider()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Bootstraper class
            catalog.Catalogs.Add(new DirectoryCatalog("."));

            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer compositionContainer = new CompositionContainer(catalog);

            ContainerProvider containerProvider = new ContainerProvider();

            //Fill the imports of this object
            try
            {
                compositionContainer.ComposeParts(containerProvider);
            }
            catch (CompositionException compositionException)
            {
                
            }

            return containerProvider;
        }
    }
}

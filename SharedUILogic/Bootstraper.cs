using Autofac;
using Core;
using SharedUILogic.Services;

namespace ReflectionApp
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

                var treeMapper = _rootScope.Resolve<MetadataTreeItemMapper>();
                var dataRepository = _rootScope.Resolve<>(new TypedParameter(typeof(TreeMapper), treeMapper));
                return _rootScope.Resolve<IMainViewModel>(new TypedParameter(typeof(IDataRepository), dataRepository));
            }
        }

        private static void Initialize()
        {
            if (_rootScope != null)
            {
                return;
            }

            _rootScope = ContainerProvider.BuildContainer("./");
        }
    }
}

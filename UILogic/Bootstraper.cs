using Autofac;
using Core;
using SharedUILogic.ViewModel;
using UILogic.ViewModel;

namespace SharedUILogic
{
    public class Bootstraper
    {
        public static MainViewModel MainViewModel
        {
            get
            {
                if (_rootScope == null)
                {
                    Initialize();
                }

                return _rootScope.Resolve<MainViewModel>();
            }
        }

        private static void Initialize()
        {
            if (_rootScope != null)
            {
                return;
            }

            //_rootScope = ContainerProvider.BuildContainer("./");
        }
    }
}

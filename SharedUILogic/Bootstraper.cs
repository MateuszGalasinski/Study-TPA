﻿using Autofac;
using Core;
using SharedUILogic.ViewModel;

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

            _rootScope = ContainerProvider.BuildContainer("./");
        }
    }
}

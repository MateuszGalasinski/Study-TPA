using Autofac;
using Core.Components;
using Core.Model;
using SharedUILogic.Model;
using SharedUILogic.Services;
using SharedUILogic.ViewModel;

namespace SharedUILogic
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TreeItemMapper>()
                .As<IMapper<AssemblyMetadataStore, TreeItem>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MainViewModel>()
                .As<IMainViewModel>()
                .SingleInstance();
        }
    }
}

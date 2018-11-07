using Autofac;
using Core.Model;
using SharedUILogic;

namespace WindowUI
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileDialog>()
                .As<IFilePathGetter>()
                .InstancePerLifetimeScope();
        }
    }
}

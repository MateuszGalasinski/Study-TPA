using Autofac;
using Core.Model;
using SharedUILogic;


namespace TreeViewConsoleApp
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleFilePathGetter>()
                .As<IFilePathGetter>()
                .InstancePerLifetimeScope();
        }
    }
}

using Autofac;
using Core.Model;
using SharedUILogic;

namespace DumpConsoleApp
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

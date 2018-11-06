using Autofac;
using Core.Components;
using Core.Model;

namespace Logging
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();
        }
    }
}

using Autofac;
using Core.Components;
using Core.Model;

namespace AssemblyReflection
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Reflector>()
                .As<IDataSourceProvider>();
        }
    }
}

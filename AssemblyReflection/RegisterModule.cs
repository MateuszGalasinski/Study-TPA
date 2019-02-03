using AssemblyReflection.ReflectorLoader;
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
                .Named<IStoreProvider>("Reflector")
                .SingleInstance();

            builder.RegisterType<DumpStoreProvider>()
                .Named<IStoreProvider>("DumpStoreProvider")
                .SingleInstance();

            builder.Register((c, p) =>
                {
                    ICompositionConfiguration compositionConfiguration = c.Resolve<ICompositionConfiguration>();
                    return c.ResolveNamed<IStoreProvider>(compositionConfiguration.ModuleVersions["IStoreProvider"]);
                })
                .As<IStoreProvider>();
        }
    }
}

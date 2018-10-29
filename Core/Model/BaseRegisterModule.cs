using Autofac;

namespace Core.Model
{
    public abstract class BaseRegisterModule : Module
    {
        protected abstract override void Load(ContainerBuilder builder);
    }
}

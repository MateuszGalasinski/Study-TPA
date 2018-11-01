using Autofac;
using Core.Model;
using ReflectionApp.ViewModels;

namespace ReflectionApp
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>()
                .As<IMainViewModel>();
        }
    }
}

using Autofac;
using Core.Model;
using ReflectionApp.Models;
using ReflectionApp.Services;
using ReflectionApp.ViewModels;

namespace ReflectionApp
{
    public class RegisterModule : BaseRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TreeMapper>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<DataRepository>()
                .As<IDataRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MainViewModel>()
                .As<IMainViewModel>()
                .SingleInstance();
        }
    }
}

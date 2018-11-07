using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

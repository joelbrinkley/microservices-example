using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Account.Logging;

namespace Account.Autofac
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoggingAdapter>().As<ILog>();
        }
    }
}

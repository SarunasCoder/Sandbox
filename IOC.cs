using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //Add autofac modules
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            builder.RegisterType<LogManager>().AsSelf();

            // Set the dependency resolver. This works for both regular
            // WCF services and REST-enabled services.
            var container = builder.Build();

            // Set the service locator to an AutofacServiceLocator.
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            //Assembly Microsoft.Practices.ServiceLocation, Version=1.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
            //https://www.nuget.org/packages/CommonServiceLocator/1.3.0
            //Assembly Autofac.Extras.CommonServiceLocator, Version = 3.2.0.0, Culture = neutral, PublicKeyToken = 17863af14b0044da
            //https://www.nuget.org/packages/Autofac.Extras.CommonServiceLocator/3.2.0
            //var logManager = ServiceLocator.Current.GetInstance<Helpers.LogManager>();

            //Wcf
            AutofacHostFactory.Container = container;
        }
    }
}

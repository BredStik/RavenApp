using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MediatR;
using Raven.Client;
using RavenApp.RequestHandlers.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RavenApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        public void BootstrapContainer()
        {
            var container = new WindsorContainer();

            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod((k, c) => {
                var store = new Raven.Client.Embedded.EmbeddableDocumentStore() { DataDirectory = "App_Data" };
                store.Initialize();
                return store;
            }));
            container.Register(Component.For<IDocumentSession>().UsingFactoryMethod((k, c) => k.Resolve<IDocumentStore>().OpenSession()).LifestylePerWebRequest());

            //mediator
            container.Register(Component.For<IMediator>().Instance(new Mediator(t => container.Resolve(t), t => container.ResolveAll(t).Cast<object>().ToArray())));
            container.Register(Classes.FromAssemblyContaining<AddNewUserCommandHandler>().BasedOn(typeof(MediatR.IRequestHandler<,>)).WithServiceAllInterfaces().LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyContaining<AddNewUserCommandHandler>().BasedOn(typeof(MediatR.IAsyncRequestHandler<,>)).WithServiceAllInterfaces().LifestylePerWebRequest());


            ControllerBuilder.Current.SetControllerFactory(new WindowControllerFactory(container));
        }
    }

    public class WindowControllerFactory: DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindowControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (_container.Kernel.HasComponent(controllerType))
            {
                return _container.Resolve(controllerType) as IController;
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }

    }
}

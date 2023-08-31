
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TRMWebUI_NET4._7._2.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TRMWebUI_NET4._7._2.App_Start.NinjectWebCommon), "Stop")]

namespace TRMWebUI_NET4._7._2.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using TRMDesktopUI.Library.Api;
    using TRMDesktopUI.Library.Models;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ILoggedInUserModel>().To<LoggedInUserModel>().InSingletonScope();
            kernel.Bind<IApiHelper>().To<ApiHelper>().InSingletonScope();
            kernel.Bind<IProductEndpoint>().To<ProductEndpoint>();
            // TODO research on .InRequestScope();
            // when necessary? what exactly is httpcontext? efficiency improvementes?

            //kernel.Bind(x =>
            //{
            //    x.FromThisAssembly()
            //     .SelectAllClasses()
            //     .BindDefaultInterface();
            //});

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}

using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using StudentMajorLeague.Web.App_Start;
using StudentMajorLeague.Web.Dependency;
using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Repositories.ChainRepository;
using StudentMajorLeague.Web.Repositories.CompetitionRepository;
using StudentMajorLeague.Web.Repositories.HistoryBlockRepository;
using StudentMajorLeague.Web.Repositories.LeagueRepository;
using StudentMajorLeague.Web.Repositories.ResultRepository;
using StudentMajorLeague.Web.Repositories.RoleRepository;
using StudentMajorLeague.Web.Repositories.UserRepository;
using StudentMajorLeague.Web.Services.ChainService;
using StudentMajorLeague.Web.Services.LeagueService;
using StudentMajorLeague.Web.Services.RoleService;
using StudentMajorLeague.Web.Services.UserService;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace StudentMajorLeague.Web.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
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
            //Bind settings
            var settings = SMLConfiguration.FromWebConfig(ConfigurationManager.AppSettings);
            kernel.Bind<SMLConfiguration>().ToConstant(settings);

            //Bind Services
            kernel.Bind<IChainReadService>().To<ChainReadService>();
            kernel.Bind<IChainWriteService>().To<ChainWriteService>();

            kernel.Bind<ILeagueReadService>().To<LeagueReadService>();
            kernel.Bind<ILeagueWriteService>().To<LeagueWriteService>();

            kernel.Bind<IRoleReadService>().To<RoleReadService>();
            kernel.Bind<IRoleWriteService>().To<RoleWriteService>();

            kernel.Bind<IUserReadService>().To<UserReadService>();
            kernel.Bind<IUserWriteService>().To<UserWriteService>();

            //Bind repositories
            kernel.Bind<IChainReadRepository>().To<ChainReadRepository>();
            kernel.Bind<IChainWriteRepository>().To<ChainWriteRepository>();

            kernel.Bind<ICompetitionReadRepository>().To<CompetitionReadRepository>();
            kernel.Bind<ICompetitionWriteRepository>().To<CompetitionWriteRepository>();

            kernel.Bind<IHistoryBlockReadRepository>().To<HistoryBlockReadRepository>();
            kernel.Bind<IHistoryBlockWriteRepository>().To<HistoryBlockWriteRepository>();

            kernel.Bind<ILeagueReadRepository>().To<LeagueReadRepository>();
            kernel.Bind<ILeagueWriteRepository>().To<LeagueWriteRepository>();

            kernel.Bind<IResultReadRepository>().To<ResultReadRepository>();
            kernel.Bind<IResultWriteRepository>().To<ResultWriteRepository>();

            kernel.Bind<IRoleReadRepository>().To<RoleReadRepository>();
            kernel.Bind<IRoleWriteRepository>().To<RoleWriteRepository>();

            kernel.Bind<IUserReadRepository>().To<UserReadRepository>();
            kernel.Bind<IUserWriteRepository>().To<UserWriteRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
        }
    }
}
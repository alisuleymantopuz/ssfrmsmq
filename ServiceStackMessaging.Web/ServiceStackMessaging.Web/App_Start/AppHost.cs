using System.Configuration;
using System.Web.Mvc;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Mvc;
using ServiceStackMessaging.Web.ServiceInterface;
using ServiceStackMessaging.Web.ServiceModel;
using ServiceStack.Redis;
using ServiceStack.Messaging.Redis;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ServiceStackMessaging.Web.AppHost), "Start")]
//More info on how to integrate with MVC: https://github.com/ServiceStack/ServiceStack/wiki/Mvc-integration

namespace ServiceStackMessaging.Web
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("ServiceStackMessaging.Web", typeof(StudentsService).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
            });

            //Config examples
            this.Plugins.Add(new PostmanFeature());
            this.Plugins.Add(new CorsFeature());

            //Redis Messaging
            container.Register<IRedisClientsManager>(new PooledRedisClientManager("localhost:6379"));

            var messageQueueService = new RedisMqServer(container.Resolve<IRedisClientsManager>());

            messageQueueService.RegisterHandler<StudentListRequest>(ServiceController.ExecuteMessage);

            messageQueueService.Start();

            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }

        public static void Start()
        {
            new AppHost().Init();
        }
    }
}

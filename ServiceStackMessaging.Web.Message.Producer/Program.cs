using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using ServiceStackMessaging.Web.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackMessaging.Web.Message.Producer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting producer!");

            var clientAppHost = new ClientAppHost();

            clientAppHost.Init();

            clientAppHost.Start("http://localhost:88/");

            while (true)
            {
                Console.WriteLine("Write student name and press enter!");
                var name = Console.ReadLine();

                clientAppHost.SearchStudent(name);

            }
        }
    }

    public class ClientAppHost : AppHostHttpListenerBase
    {
        public RedisMqServer RedisMqServer { get; set; }
        public ClientAppHost()
            : base("Producer Console App", typeof(StudentListRequest).Assembly)
        {

        }
        public override void Configure(Funq.Container container)
        {
            //local redis server
            var redisFactory = new PooledRedisClientManager("localhost:6379");

            RedisMqServer = new RedisMqServer(redisFactory);

            //for hadling response
            RedisMqServer.RegisterHandler<StudentListResponse>(message =>
            {
                var id = message.GetBody().GetId();

                var studentsCount = message.GetBody().GetDto<StudentListResponse>().Students.Count;

                Console.WriteLine("Handled message id : {0}, students count : {1}", id, studentsCount);

                return null;
            });

            //redis message queue server start.
            RedisMqServer.Start();
        }

        public void SearchStudent(string name)
        {
            //search operation and producing
            using (var client = RedisMqServer.CreateMessageQueueClient())
            {
                //for each operation uniqe key
                var uniqeQ = "mq:c1" + ":" + Guid.NewGuid().ToString("N");

                var message = new Message<StudentListRequest>(new StudentListRequest() { Name = name }) { ReplyTo = uniqeQ };

                client.Publish(message);

                //Getting response by key

                //var response = client.Get<StudentListResponse>(uniqeQ, new TimeSpan(0, 1, 0, 0));

                //Console.WriteLine("Search result count : {0}", response.GetBody().GetResponseDto<StudentListResponse>().Students.Count);
            }
        }

    }
}

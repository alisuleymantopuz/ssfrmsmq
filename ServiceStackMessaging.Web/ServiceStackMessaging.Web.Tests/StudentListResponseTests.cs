using System;
using NUnit.Framework;
using ServiceStackMessaging.Web.ServiceInterface;
using ServiceStackMessaging.Web.ServiceModel;
using ServiceStack.Testing;
using ServiceStack;

namespace ServiceStackMessaging.Web.Tests
{
    [TestFixture]
    public class StudentListResponseTests
    {
        private readonly ServiceStackHost appHost;

        public StudentListResponseTests()
        {
            appHost = new BasicAppHost(typeof(StudentsService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void TestMethod1()
        {
            var service = appHost.Container.Resolve<StudentsService>();

            var response = (StudentListResponse)service.Any(new StudentListRequest { Name = "andrew" });

            Assert.NotNull(response.Students);

            Assert.AreEqual(response.Students.Count, 1);
        }
    }
}

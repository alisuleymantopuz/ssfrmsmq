using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ServiceStackMessaging.Web.ServiceModel
{
    [Route("/students")]
    [Route("/students/{Name}")]
    public class StudentListRequest : IReturn<StudentListResponse>
    {
        public string Name { get; set; }
    }

}
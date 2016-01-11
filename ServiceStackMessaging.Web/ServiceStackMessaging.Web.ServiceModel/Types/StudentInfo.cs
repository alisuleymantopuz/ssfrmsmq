using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackMessaging.Web.ServiceModel.Types
{
    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int CurrentClass { get; set; }
    }
}

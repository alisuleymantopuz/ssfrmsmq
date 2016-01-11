using ServiceStackMessaging.Web.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackMessaging.Web.ServiceModel
{
    public class StudentListResponse
    {
        public IList<StudentInfo> Students { get; set; }
    }
}

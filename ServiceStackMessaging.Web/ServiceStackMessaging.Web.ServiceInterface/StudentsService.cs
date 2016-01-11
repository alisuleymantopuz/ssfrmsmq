using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStackMessaging.Web.ServiceModel;
using ServiceStackMessaging.Web.ServiceModel.Types;

namespace ServiceStackMessaging.Web.ServiceInterface
{
    public class StudentsService : Service
    {
        public object Any(StudentListRequest request)
        {
            var students = StudentInfoListFactory.CreateStudentList()
                                .Where(x => x.Name.StartsWith(request.Name, StringComparison.OrdinalIgnoreCase)
                                    || x.Name.ToLower().Contains(request.Name)).ToList();

            return new StudentListResponse { Students = students };
        }
    }

    public static class StudentInfoListFactory
    {
        public static IList<StudentInfo> CreateStudentList()
        {
            var students = new List<StudentInfo>()
            {
                new StudentInfo(){StudentId=1,Name="Andrew",City="Boston",CurrentClass=2},
                new StudentInfo(){StudentId=2,Name="Richa",City="Chicago",CurrentClass=3},
                new StudentInfo(){StudentId=3,Name="Dave",City="Phoenix",CurrentClass=4},
                new StudentInfo(){StudentId=4,Name="Ema",City="Washington",CurrentClass=5},
                new StudentInfo(){StudentId=5,Name="Filip",City="Texas",CurrentClass=6},
                new StudentInfo(){StudentId=6,Name="Maggi",City="Los Angeles",CurrentClass=7},
                new StudentInfo(){StudentId=7,Name="Nathan",City="Atlanta",CurrentClass=8}
            };

            return students;
        }
    }
}
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class UserIris: UserV2
    {
        public string DepartmentTagIris { get; set; }
        public string FirstNameIris { get; set; }
        public string LastNameIris { get; set; }
        public string MiddleNameIris { get; set; }
        public string LoginIris { get; set; }
        public string PositionIris { get; set; }

    }
}

using Diadoc.Api.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class DepartmentIris: Department
    {
        public string DepartNameIris { get; set; }

        public string AbbreviationIris { get; set; }

        public string KppIris { get; set; }

        public string ParentDepartmentIdIris { get; set; }

        public string AddressIris { get; set; }
    }
}

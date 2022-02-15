using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class WorkWithDocSign
    {
        public int CountContractor { get; set; }
        public int CountUserIris { get; set; }
        public int CountDepartmentIris { get; set; }
        public string ListContragent { get; set; }
        public string DocforWork { get; set; }
        public string DocumentName { get; set; }
        public string ResolutionStatus { get; set; }
        public string ErrorMes { get; set; }
    }
}

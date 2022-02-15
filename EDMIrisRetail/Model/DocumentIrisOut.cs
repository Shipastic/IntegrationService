using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class DocumentIrisOut
    {
        public string Status { get; set; }
        public string Title { get; set; }
        public string FromDep { get; set; }
        public string EntityID { get; set; }
        public string MessageID { get; set; }
        public string ShortName { get; set; }
        public string ToDep { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class RequisitesDocumentSender
    {
        public string INN { get; set; }
        public string KPP { get; set; }
        public string NameOrg { get; set; }
        public string City { get; set; }
        public string House { get; set; }
        public string Index { get; set; }
        public string CodeRegion { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string Corpus { get; set; }
        public string Currency { get; set; }
        public string AddrCompany { get; set; }

        public string GetAddress(RequisitesDocumentSender requisites)
        {
            string requisitesAddr = $"{requisites?.City??      "" } " +
                                    $"{requisites?.Street??    "" } " +
                                    $"{requisites?.House??     "" } " +
                                    $"{requisites?.Corpus ??   "" } " +
                                    $"{requisites?.Apartment?? "" } " ;

            return requisitesAddr;
        }

        public string GetAddress(string ValueAttr)
        {
            string requisitesAddr = ValueAttr;

            return requisitesAddr;
        }
    }
}

using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class ParcedDocument
    {
        public Int64 Ids { get; set; }
        public string entityId { get; set; }
        public string messageId { get; set; }
        public int status { get; set; }
        public string email { get; set; }
        public string cntTag { get; set; }
        public string errMsg { get; set; }

        public string DocType { get; set; }

        public string DocumentIn { get; set; }

        public DateTime InsertDateDoc { get; set; }
        public string statusParce { get; set; }
        public string statusParcePrint { get; set; }
        public byte isArrove { get; set; }
        public string NameFile { get; set; }
        public string DocName { get; set; }

        public int flagExistDoc = 0;
    }
}

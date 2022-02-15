using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class DocumentsMoveOperationIris : DocumentsMoveOperation
    {      
        public DocumentsMoveOperationIris()
        {

        }
        public DocumentsMoveOperationIris(string boxid, string departid)
        {
            BoxId = boxid;

            ToDepartmentId = departid;
        }

        public DocumentsMoveOperation SetMoveOperation(string boxid, string departid)
        {
            DocumentsMoveOperation moveOperation = new DocumentsMoveOperation
            {
                BoxId = boxid,
                ToDepartmentId = departid
            };
            return moveOperation;

        }
    }
}

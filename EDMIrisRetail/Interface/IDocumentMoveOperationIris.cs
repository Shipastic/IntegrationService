using Diadoc.Api.Proto.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IDocumentMoveOperationIris
    {
        void MoveDocIris(DocumentsMoveOperation documentsMoveOperation, Int64 docId);
    }
}

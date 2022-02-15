using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using EDMIrisRetail.Interface;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
   public class DocumentMoveOperationIrisController: IDocumentMoveOperationIris
    {
        OracleConnection connectionState =OracleConnectionState.GetInstance();

        OracleConnectionState state;
        public void MoveDocIris(DocumentsMoveOperation documentsMoveOperation, Int64 docId)
        {
            try
            {
                EDMClass.apiDiadoc.MoveDocuments(EDMClass.authTokenByLogin, documentsMoveOperation);
            }
            catch (HttpClientException ex)
            {
                OracleConnectionState.SetLog(docId, ex.AdditionalMessage.ToString(), Constants.DirectOtherDep);

                //logger.Error(ex, ex.AdditionalMessage.ToString());
            }
        }
    }
}

using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class MessageIris
    {
        /// <summary>
        /// Метод получения всей цепочки сообщений для конкретного документа
        /// </summary>
        /// <param name="document"> документ </param>
        /// <returns></returns>
        public Message GetMessageED(Document document)
        {
            Message mes = EDMClass.apiDiadoc.GetMessage(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, document.MessageId, false, true);

            return mes;
        }
    }
}

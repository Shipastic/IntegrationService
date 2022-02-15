using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IRequisitesDocumentSender
    {
        RequisitesDocumentSender GetDataFromDocumentSender(List<Content> contents, Document document, Message message);
    }
}

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
    interface IRequisitesDocumentRecipient
    {
        RequisitesDocumentRecipient GetDataFromDocumentRecipient(List<Content> contents, Document document, Message message);
    }
}

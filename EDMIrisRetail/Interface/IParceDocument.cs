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
    interface IParceDocument
    {
        ParcedDocument GetParcedDocument(Contractor contractor, string lastIndex, string document, string messageId, string entityId, out int docExist);

        ParcedDocument GetParcedDocumentWithSign(Contractor contractor, string lastIndex, string document, string messageId, string entityId);

        ParcedDocument ExecParceDoc(Contractor contractor, Document document, Message message, List<Content> contents, ParcedDocument parcedDocument);

        ParcedDocument ExecParceDocWithSign(Contractor contractor, Document document, Message message, List<Content> contents, ParcedDocument parcedDocument);

        ParcedDocument FillDoc(long idDoc);


    }
}

using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EDMIrisRetail.Interface
{
    interface IDocumentIrisIn
    {
        /// <summary>
        /// Метод для получения всех документов согласно фильтра
        /// </summary>
        /// <param name="documentsFilter"></param>
        /// <returns></returns>
        DocumentList GetDocumentEDOs(DocumentsFilterIris documentsFilter);

        /// <summary>
        /// Метод для получения документов без повторной обработки
        /// </summary>
        /// <param name="docReloads"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        DocumentList GetDocInbountReload(List<DocReload> docReloads, DocumentList list);

        /// <summary>
        /// Метод получения списка документов по контрагентам по заданным условиям
        /// </summary>
        /// <param name="documentList"> документы без повторной обработки</param>
        /// <returns></returns>
        List<Document> GetListCounteragents(DocumentList documentList);

        bool DocumentExist(string messageId, string entityId);

        bool DocExistWithRUAPPROVEStatus1(string messageID, string entityID);

        bool DocExistWithRUAPPROVEStatus2(string messageId, string entityId);

        long GetId(string tableName);

        int GetIdRuStatus1(string messageID, string entityID);

        int GetIdRuStatus2(string messageId, string entityId);

        void setDocument(Contractor contractor, string lastIndex, string document, long idDoc, string messageId, string entityId);

        List<DocReload> GetDocReloads(Int64 cntId);

        List<DocumentIrisIn> GetDocumentInbIris(List<Contractor> contractors, List<DepartmentIris> departments);

       // List<DocumentIrisIn> GetDocumentOutbIris(List<Contractor> contractors, List<DepartmentIris> departments);
    }
}

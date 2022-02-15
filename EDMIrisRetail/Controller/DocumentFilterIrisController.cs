using Diadoc.Api;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
    class DocumentFilterIrisController : IDocumentFilter
    {
        public DateTime getDate(Contractor contractor)
        {
            return contractor.dateStart > DateTime.Now.AddDays(-50) ? contractor.dateStart : DateTime.Now.AddDays(-50);
        }

        public DateTime getDateForSigned(Contractor contractor)
        {
            return contractor.dateStart > DateTime.Now.AddDays(-5) ? contractor.dateStart : DateTime.Now.AddDays(-5);
        }

        public DocumentsFilterIris SetFilterCurrContractorInbound(string _boxId, string _categoryFilter, string _contreagentboxId, DateTime _date, Contractor _contractor)
        {
            DocumentsFilterIris documentsFilterIris = new DocumentsFilterIris
            {
                BoxIdIris = _boxId,
                CounteragentBoxIdIris = _contreagentboxId,
                FilterCategoryIris = _categoryFilter,
                TimestampFromIris = _date,
                contractorNameIris = _contractor.Name,
                contractor = _contractor
            };
            return documentsFilterIris;
        }

        public DocumentsFilter SetFilterCurrContractorInboundDiadoc(string _boxId, string _categoryFilter, string _contreagentboxId, DateTime _date)
        {
            DocumentsFilter documentsFilterIris = new DocumentsFilter
            {
                BoxId = _boxId,
                CounteragentBoxId = _contreagentboxId,
                FilterCategory = _categoryFilter,
                TimestampFrom = _date
            };
            return documentsFilterIris;
        }
    }
}

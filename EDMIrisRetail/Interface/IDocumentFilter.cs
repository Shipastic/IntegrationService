using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IDocumentFilter
    {
        DateTime getDate(Contractor contractor);

        DateTime getDateForSigned(Contractor contractor);

        DocumentsFilterIris SetFilterCurrContractorInbound(string _boxId, string _categoryFilter, string _contreagentboxId, DateTime _date, Contractor _contractor);

    }
}

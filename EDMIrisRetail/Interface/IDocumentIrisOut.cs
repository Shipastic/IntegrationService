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
    interface IDocumentIrisOut
    {
        List<DocumentIrisOut> GetDocumentOutbIris(List<Contractor> contractors, List<DepartmentIris> departments);
    }
}

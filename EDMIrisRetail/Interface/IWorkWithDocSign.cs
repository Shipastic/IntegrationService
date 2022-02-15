using Diadoc.Api.Proto.Documents;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IWorkWithDocSign
    {
        WorkWithDocSign SetCommentForDocEithParamFilter(Contractor contr, DocumentList ListDoc, WorkWithDoc workWithDoc);
    }
}

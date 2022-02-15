using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IStateDocument
    {
        string SetFirstStateDocument(ParcedDocument parcedDocument);

        string SetSecondStatedocument(ParcedDocument parcedDocument);
    }
}

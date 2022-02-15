using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IRequisitesDocumentFromQNTSOFT
    {
        string GetINNFromDocumentSender(string tokenQNTSOFT, string innOrg);

        RequisitesDocumentFromQNTSOFT GetRequisitesFromQNTSOFT(string tokenQNTSOFT, string innOrg);

    }
}

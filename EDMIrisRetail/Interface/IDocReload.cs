using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IDocReload
    {
        List<DocReload> GetDocReloads(Contractor contractor);
    }
}

using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
   public class DocReloadController: IDocReload
    {
        DocumentIrisController documentRepository = new DocumentIrisController();
        public List<DocReload> GetDocReloads(Contractor contractor)
        {
            return documentRepository.GetDocReloads(contractor.Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class Contractor
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string BoxId { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string LastIndexKey { get; set; }
        public DateTime dateStart { get; set; }
    }
}

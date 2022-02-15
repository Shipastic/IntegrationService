using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class RequisitesDocumentFromQNTSOFT
    {
        public string description { get; set; }
        public bool ok { get; set; }
        public string inn { get; set; }
        public bool valid { get; set; }
        public float process_time { get; set; }
        public Legal_Entities[] legal_entities { get; set; }

        public string GetNameShortOrg(string opf_full, string name_full)
        {

            string resNameShort = $"{Regex.Replace(opf_full, "\"", "")} \"{name_full}\"";

            return resNameShort;
        }
    }
    public class Legal_Entities
        {
        public string name { get; set; }
        public string kpp { get; set; }
        public object capital { get; set; }
        public string head { get; set; }
        public string head_position { get; set; }
        public object founders { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string actuality_date { get; set; }
        public string registration_date { get; set; }
        public string liquidation_date { get; set; }
        public string opf_full { get; set; }
        public string opf_short { get; set; }
        public string name_full { get; set; }
        public string name_short { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string okpo { get; set; }
        public string okved { get; set; }
        public object okveds { get; set; }
        public string address { get; set; }
        public object phones { get; set; }
        public object emails { get; set; }
        public long? ogrn_date { get; set; }
    
    }


}

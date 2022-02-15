using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
    public class RequisitesDocumentFromQNTSOFTController : IRequisitesDocumentFromQNTSOFT
    {
        /// <summary>
        /// Метод для проверки ИНН  через апи
        /// </summary>
        /// <param name="tokenQNTSOFT"></param>
        /// <param name="innOrg"></param>
        /// <returns></returns>
        public string GetINNFromDocumentSender(string tokenQNTSOFT, string innOrg)
        {
            var BaseUrl = new Uri("https://scoring.qntsoft.ru/api/ru/validate/inn");

            RequisitesDocumentFromQNTSOFT qNTSOFT = new RequisitesDocumentFromQNTSOFT();

            NameValueCollection nameValueCollection = new NameValueCollection
            {
                { "token", tokenQNTSOFT },
                { "inn", innOrg }
            };

            using (WebClient newClient = new WebClient() { Encoding = Encoding.UTF8})
            {
                var bytes = newClient.UploadValues(BaseUrl, nameValueCollection);

                string str = Encoding.Default.GetString(bytes);

                qNTSOFT = JsonConvert.DeserializeObject<RequisitesDocumentFromQNTSOFT>(str);
            }
            if (qNTSOFT.valid)
                return qNTSOFT.inn;
            else
                return null;
        }

        /// <summary>
        /// Метод для установки значений свойств класса RequisitesDocumentFromQNTSOFT 
        /// </summary>
        /// <param name="tokenQNTSOFT"></param>
        /// <param name="innOrg"></param>
        /// <returns></returns>
        public RequisitesDocumentFromQNTSOFT GetRequisitesFromQNTSOFT(string tokenQNTSOFT, string innOrg)
        {
            var BaseUrl = new Uri("https://scoring.qntsoft.ru/api/ru/egrul/inn");

            RequisitesDocumentFromQNTSOFT fromQNTSOFT = new RequisitesDocumentFromQNTSOFT();

            NameValueCollection nameValueCollection = new NameValueCollection
            {
                { "token", tokenQNTSOFT },
                { "inn", innOrg }
            };

            using (WebClient newClient = new WebClient())
            {
                byte[] bytes = newClient.UploadValues(BaseUrl, nameValueCollection);

                string str = Encoding.UTF8.GetString(bytes);

                fromQNTSOFT = JsonConvert.DeserializeObject<RequisitesDocumentFromQNTSOFT>(str);
   
            }
            return fromQNTSOFT;
        }
    }
}

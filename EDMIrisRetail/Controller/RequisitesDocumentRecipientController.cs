using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EDMIrisRetail.Controller
{
    public class RequisitesDocumentRecipientController : IRequisitesDocumentRecipient
    {
        public RequisitesDocumentRecipient GetDataFromDocumentRecipient(List<Content> contents, Document document, Message message)
        {
            RequisitesDocumentRecipient requisites = new RequisitesDocumentRecipient();

            #region парсинг xml

            string pathFile = @"\\192.168.48.25\reports_recieve\diadocValidData.xml";

            foreach (var cont in contents.Where(c => c.Data != null))
            {

                using (FileStream fileStream1 = new FileStream(pathFile, FileMode.Create))
                {
                    fileStream1.Write(cont.Data, 0, cont.Size);
                }

                XDocument xLDoc = XDocument.Load(pathFile);

                ///Выбор КПП ИНН и Названия организации получателя
                if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("ИдСв").Elements("СвЮЛУч").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("ИдСв").Elements("СвЮЛУч"))
                    {
                        requisites.KPP = dataElement.Attribute("КПП").Value;
                        requisites.INN = dataElement.Attribute("ИННЮЛ").Value;
                        requisites.NameOrg = dataElement.Attribute("НаимОрг").Value;
                    }
                }
                else
                if(xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("ИдСв").Elements("СвЮЛ").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("ИдСв").Elements("СвЮЛ"))
                    {
                        requisites.KPP = dataElement.Attribute("КПП").Value;
                        requisites.INN = dataElement.Attribute("ИННЮЛ").Value;
                        requisites.NameOrg = dataElement.Attribute("НаимОрг").Value;
                    }
                }
                else
                {
                    requisites.KPP = "Номер КПП получателя не найден";
                    requisites.INN = "Номер ИНН получателя не найден";
                    requisites.NameOrg = "Название организации получателя не найдено";
                }

                ///Выбор адреса организации получателя
                if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("Адрес").Elements("АдрРФ").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("Адрес").Elements("АдрРФ"))
                    {
                        requisites.House = dataElement.Attribute("Дом")?.Value ?? "";
                        requisites.Index = dataElement.Attribute("Индекс")?.Value ?? "";
                        requisites.Apartment = dataElement.Attribute("Кварт")?.Value ?? "";
                        requisites.CodeRegion = dataElement.Attribute("КодРегион")?.Value ?? "";
                        requisites.Street = dataElement.Attribute("Улица")?.Value ?? "";
                        requisites.Corpus = dataElement.Attribute("Корпус")?.Value ?? "";
                        requisites.City = dataElement.Attribute("Город")?.Value ?? "";
                        //AddrCompany  - используется здесь как флаг для дальнейшей проверки
                        requisites.AddrCompany = null;
                    }
                }
                else
                    if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("Адрес").Elements("АдрИнф").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПокуп").Elements("Адрес").Elements("АдрИнф"))
                    {
                        requisites.AddrCompany = requisites.GetAddress(dataElement.Attribute("АдрТекст").Value);
                    }
                }
                else
                {
                    requisites.AddrCompany = "Адрес компании получателя не найден";
                }
            }

            #endregion
            return requisites;
        }
    }
}

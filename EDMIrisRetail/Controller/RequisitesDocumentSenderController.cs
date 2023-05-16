using Diadoc.Api;
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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EDMIrisRetail.Controller
{
    public class RequisitesDocumentSenderController : IRequisitesDocumentSender
    {
        /// <summary>
        /// Метод для извлечения реквизитов из документа
        /// </summary>
        /// <param name="contents">содержимое файла</param>
        /// <returns></returns>
        public RequisitesDocumentSender GetDataFromDocumentSender(List<Content> contents, Document document, Message message)
        {
            RequisitesDocumentSender requisites = new RequisitesDocumentSender();

            #region парсинг xml

            string pathFile = @"\\ip_address\reports_recieve\diadocValidData.xml";

            foreach (var cont in contents.Where(c => c.Data != null))
            {

                using (FileStream fileStream1 = new FileStream(pathFile, FileMode.Create))
                {
                    fileStream1.Write(cont.Data, 0, cont.Size);
                }

                XDocument xLDoc = XDocument.Load(pathFile);

                ///Выбор КПП ИНН и Названия организации отправителя
                if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("ИдСв").Elements("СвЮЛУч").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("ИдСв").Elements("СвЮЛУч"))
                    {
                        requisites.KPP = dataElement.Attribute("КПП").Value;
                        requisites.INN = dataElement.Attribute("ИННЮЛ").Value;
                        requisites.NameOrg = dataElement.Attribute("НаимОрг").Value;
                    }
                }
                else
                if(xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("ИдСв").Elements("СвЮЛ").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("ИдСв").Elements("СвЮЛ"))
                    {
                        requisites.KPP = dataElement.Attribute("КПП").Value;
                        requisites.INN = dataElement.Attribute("ИННЮЛ").Value;
                        requisites.NameOrg = dataElement.Attribute("НаимОрг").Value;
                    }
                }
                else
                {
                    requisites.KPP = "Номер КПП отправителя не найден";
                    requisites.INN = "Номер ИНН отправителя не найден";
                    requisites.NameOrg = "Название организации отправителя не найдено";
                }
                ///Выбор адреса организации отправителя
                if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрРФ").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрРФ"))
                    {
                        requisites.House      = dataElement.Attribute("Дом")?.Value?? "";
                        requisites.Index      = dataElement.Attribute("Индекс")?.Value?? "";
                        requisites.Apartment  = dataElement.Attribute("Кварт")?.Value?? "";
                        requisites.CodeRegion = dataElement.Attribute("КодРегион")?.Value?? "";
                        requisites.Street     = dataElement.Attribute("Улица")?.Value ?? "";
                        requisites.Corpus     = dataElement.Attribute("Корпус")?.Value ?? "";
                        requisites.City       = dataElement.Attribute("Город")?.Value ?? "";
                        //AddrCompany  - используется здесь как флаг для дальнейшей проверки
                        requisites.AddrCompany = null;
                    }
                }
                else
                    if(xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрИнф").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрИнф"))
                    {
                      requisites.AddrCompany = requisites.GetAddress(dataElement.Attribute("АдрТекст").Value);
                    }
                }
                else
                {
                    requisites.AddrCompany = "Адрес компании отправителя не найден";
                }

                ///Выбор кода валюты
                foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт"))
                {
                    requisites.Currency = dataElement.Attribute("КодОКВ").Value;
                }
            }
            #endregion
            //================================
            #region парсинг XML с помощью PDFFocus
            //PrintFormResult formResult;

            //string pathFilePrint = "";

            ////Создаем каталог для хранения файлов печатных форм
            //var dir = @"\\192.168.48.25\reports_recieve\PrintForm";

            //string pathFile = @"\\ip_address\reports_recieve\diadocValidData.xml";

            //foreach (var cont in contents.Where(c => c.Data != null))
            //{
            //    do
            //{
            //    formResult = EDMClass.apiDiadoc.GeneratePrintForm(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, message.MessageId, document.EntityId);

            //      var secondsToWait = formResult.RetryAfter;

            //    //Ожидаем завершения создания печатной формы
            //    if (secondsToWait > 0)
            //    {
            //        Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
            //    }
            //} while (!formResult.HasContent);

            //var contentFileName = formResult.Content.FileName;

            // pathFilePrint = $"{dir}\\{contentFileName}";

            
            #endregion
            //================================

            return requisites;
        }
    }
}

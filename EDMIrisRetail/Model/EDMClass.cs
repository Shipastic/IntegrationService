using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EDMIrisRetail
{
    public class EDMClass
    {
        ///Экземпляр класса АПИ Диадока    
        private static DiadocApi _diadocApi;

        ///Токен    
        private static string _authTokenByLogin;


        private static DateTime _authTokenDate;

        public static DiadocApi apiDiadoc
        {
            get
            {
                if (_diadocApi == null)
                {
                    var crypt = new WinApiCrypt();
                    _diadocApi = new DiadocApi(
                      Constants.DefaultClientId,
                      Constants.DefaultApiUrl,
                      crypt);
                }
                return _diadocApi;
            }
        }
        private static bool authTokenOld()
        {
            if (_authTokenDate == null)
            {
                return true;
            }
            else
            {
                TimeSpan ts = DateTime.Now - _authTokenDate;
                if (ts.TotalHours > 3)
                {
                    return true;
                }
            }
            return false;
        }
        public static string authTokenByLogin
        {
            get
            {
                try
                {
                    if (_authTokenByLogin == null || authTokenOld())
                    {
                        _authTokenByLogin = apiDiadoc.Authenticate(Constants.LogIn,
                                                                   Constants.PassWord);
                        _authTokenDate = DateTime.Now;
                    }
                    return _authTokenByLogin;
                }
                catch(Diadoc.Api.Http.HttpClientException ex)
                {
                    ErrorWindow errorWindow = new ErrorWindow();
                    errorWindow.errorMessage.Text = ex.AdditionalMessage;
                    errorWindow.erMessText.Text = ex.AdditionalMessage;
                    errorWindow.ShowDialog();

                    return _authTokenByLogin;
                }          
            }
        }

        /// <summary>
        /// Метод для добавления дополнения к сообщению по ИД
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public MessagePatch PostMessagePatch(MessagePatchToPost msg, Int64 docId) //TODO добавить WorkWithDOc в параметры и в поле ErrorMes установыить текст ошибки
        {
            MessagePatch retVal = new MessagePatch();
            try
            {
                retVal = apiDiadoc.PostMessagePatch(authTokenByLogin, msg);
            }
            catch (Diadoc.Api.Http.HttpClientException ex)
            {
                OracleConnectionState.SetLog(docId, ex.AdditionalMessage.ToString(), "Отправка на согласование");

                // logger.Error(ex, ex.AdditionalMessage.ToString());
            }
            return retVal;
        }

        /// <summary>
        /// Метод для добавления дополнения ко всем сообщениям
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public MessagePatch PostMessagePatch(MessagePatchToPost msg) //TODO добавить WorkWithDOc в параметры и в поле ErrorMes установыить текст ошибки
        {
            MessagePatch retVal = new MessagePatch();
            try
            {
                retVal = apiDiadoc.PostMessagePatch(authTokenByLogin, msg);
            }
            catch (Diadoc.Api.Http.HttpClientException ex)
            {

                // logger.Error(ex, ex.AdditionalMessage.ToString());
            }
            return retVal;
        }


        /// <summary>
        /// Метод для перемещения документов в заданное подразделение
        /// </summary>
        /// <param name="dmo">Структура, содержащая информацию, что куда и откуда переместить</param>
        /// <param name="docId"></param>
        public void MoveDocuments(DocumentsMoveOperation dmo, Int64 docId)
        {

            try
            {
                apiDiadoc.MoveDocuments(authTokenByLogin, dmo);
            }
            catch (Diadoc.Api.Http.HttpClientException ex)
            {
                // ora.SetLog(docId, ex.AdditionalMessage.ToString(), "Отправка в другое подразделение");
                // logger.Error(ex, ex.AdditionalMessage.ToString());
            }
        }

        /// <summary>
        /// Метод для генерации печатной формы
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="messageId"></param>
        /// <param name="documentId"></param>
        public string GeneratePrintForm(string boxId, string messageId, string documentId, Contractor contractor, string title)
        {
            string pathFilePrint = "";
            //Создаем каталог для хранения файлов печатных форм
            var dir = new DirectoryInfo(@"\\192.168.48.25\reports_recieve\PrintForm");

            if (!dir.Exists)
            {
                dir.Create();
            }

            PrintFormResult formResult;
            try
            {
                do
                {
                    formResult = apiDiadoc.GeneratePrintForm(authTokenByLogin, boxId, messageId, documentId);

                    var secondsToWait = formResult.RetryAfter;

                    //Ожидаем завершения создания печатной формы
                    if (secondsToWait > 0)
                    {
                        // logger.Info($"Печатная форма еще не готова, ожидаем {secondsToWait} секунд");

                        Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
                    }
                } while (!formResult.HasContent);

                //Имя файла    
                var contentFileName = formResult.Content.FileName;

                pathFilePrint = $"Печатная форма готова, находится в папке {dir.Name}. Имя файла: {contentFileName}";

                //Создаем файл   
                File.WriteAllBytes($"{dir}\\{contentFileName}", formResult.Content.Bytes);

                OracleConnectionState.WritePrintFormToDB(formResult.Content.Bytes, contractor.Id, documentId, title);
            }
            catch (Exception ex)
            {
                //logger.Error(ex, ex.Message);
            }

            return pathFilePrint;
        } 
    }
}

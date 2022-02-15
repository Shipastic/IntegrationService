using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace EDMIrisRetail.Controller
{
    public class ParcedDocumentController : IParceDocument
    {
        DocumentIrisController documentRepository = new DocumentIrisController();

        OracleConnection oraQwery = OracleConnectionState.GetInstance();

        // OracleConnection connection;

        EDMClass eDMClass = new EDMClass();

        public int flagExistDoc = 0;

        /// <summary>
        /// Метод для установки ИД документа, установки полей класса ParcedDocument
        /// </summary>
        /// <param name="contractor"> поставщик </param>
        /// <param name="lastIndex"></param>
        /// <param name="document"> текущий документ </param>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <param name="docExist"> флаг наличия документа в бд </param>
        /// <param name="statusParce"></param>
        /// <returns></returns>
        public ParcedDocument GetParcedDocument(Contractor contractor, string lastIndex, string document, string messageId, string entityId, out int docExist)
        {
            //Если документа не существует         
            if (documentRepository.DocumentExist(messageId, entityId) == false)
            {
                docExist = 1;

                //Генерируем ID для строки
                Int64 idDoc = documentRepository.GetId("iris_diadoc_doc");

                documentRepository.setDocument(contractor, lastIndex, document, idDoc, messageId, entityId);

                ParcedDocument parced = FillDoc(idDoc);

                return parced;
            }
            else
            if (documentRepository.DocExistWithRUAPPROVEStatus1(messageId, entityId))
            {
                docExist = 0;

                //Получаем ИД из базы
                Int32 ids = documentRepository.GetIdRuStatus1(messageId, entityId);

                ParcedDocument retDoc = FillDoc(ids);

                return retDoc;
            }

            docExist = 0;

            return null;
        }

        /// <summary>
        /// Метод для проверки документов с подписями
        /// </summary>
        /// <param name="contractor"></param>
        /// <param name="lastIndex"></param>
        /// <param name="document"></param>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <param name="statusParce"></param>
        /// <returns></returns>
        public ParcedDocument GetParcedDocumentWithSign(Contractor contractor, string lastIndex, string document, string messageId, string entityId)
        {
            if (documentRepository.DocExistWithRUAPPROVEStatus2(messageId, entityId))
            {
                Int32 ids = documentRepository.GetIdRuStatus2(messageId, entityId);

                ParcedDocument retDocSign = FillDoc(ids);

                return retDocSign;
            }
            return null;
        }



        public ParcedDocument ExecParceDoc(Contractor contractor, Document document, Message message, List<Content> contents, ParcedDocument parcedDocument)
        {

            ///Если содержимое сообщения не пусто
            foreach (var cont in contents.Where(c => c.Data != null))
            {
                string str = Encoding.Default.GetString(cont.Data);                

                parcedDocument = GetParcedDocument(contractor, document.IndexKey, str, message.MessageId, document.EntityId, out flagExistDoc);

                ///Формируем печатные формы
                if (parcedDocument != null)
                {
                    parcedDocument.statusParcePrint = eDMClass.GeneratePrintForm(Constants.DefaultFromBoxId, message.MessageId, document.EntityId, contractor, document.Title);
                    if (flagExistDoc == 1 && parcedDocument != null)
                    {
                        parcedDocument.statusParce += $"{parcedDocument.NameFile} : {Constants.newdocForDB}";
                    }

                    else
                    {
                        parcedDocument.statusParce += $"{parcedDocument.NameFile} : {Constants.olddocForDB}";
                    }
                }
                else
                {
                    string statusPrint = eDMClass.GeneratePrintForm(Constants.DefaultFromBoxId, message.MessageId, document.EntityId, contractor, document.Title);
                }
             
            }

            return parcedDocument;
        }

        public ParcedDocument ExecParceDocWithSign(Contractor contractor, Document document, Message message, List<Content> contents, ParcedDocument parcedDocument)
        {
            foreach (var cont in contents.Where(c => c.Data != null))
            {
                string str = Encoding.Default.GetString(cont.Data);

                parcedDocument = GetParcedDocumentWithSign(contractor, document.IndexKey, str, message.MessageId, document.EntityId);
                if (parcedDocument != null)
                {
                    parcedDocument.statusParcePrint = eDMClass.GeneratePrintForm(Constants.DefaultFromBoxId, message.MessageId, document.EntityId, contractor, document.Title);

                    parcedDocument.statusParcePrint += $"\nДля документа {parcedDocument.NameFile} обновлена печатная форма";
                }
                else
                {
                    string statusPrint = eDMClass.GeneratePrintForm(Constants.DefaultFromBoxId, message.MessageId, document.EntityId, contractor, document.Title);
                }
            }

            return parcedDocument;
        }

        /// <summary>
        /// Метод для установки полей класса ParcedDocument для определенного документа
        /// </summary>
        /// <param name="idDoc"> ИД текущего документа </param>
        /// <returns></returns>
        public ParcedDocument FillDoc(long idDoc)
        {

            string _status = "";
            ParcedDocument parcedDocument = new ParcedDocument();
            DataSet dataSetResult = new DataSet();
            var cmdSelectDoc = oraQwery.CreateCommand();
            cmdSelectDoc.CommandText = @" SELECT status,email,cnt_tag,RU_APPROVE,entity_id, print_file_name,'' error FROM iris_diadoc_doc WHERE id = :id";
            using (cmdSelectDoc)
            {
                try
                {
                    cmdSelectDoc.Connection = oraQwery;
                    cmdSelectDoc.BindByName = true;
                    cmdSelectDoc.Parameters.Add("id", idDoc);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmdSelectDoc))
                    {
                        da.Fill(dataSetResult);
                        DataRow row = dataSetResult.Tables[0].Rows[0];
                        parcedDocument = new ParcedDocument
                        {
                            Ids = idDoc,
                            cntTag = row["cnt_tag"].ToString(),
                            email = "informer-iris@iris-retail.ru",//row["email"].ToString(),
                            entityId = row["entity_id"].ToString(),
                            status = int.Parse(row["status"].ToString()),
                            isArrove = byte.Parse(row["RU_APPROVE"].ToString()),
                            NameFile = row["print_file_name"].ToString(),
                            statusParce = "",
                            statusParcePrint = "",
                            errMsg = ""

                        };
                    }
                }
                catch (OracleException e)
                {
                    //logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                    //logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(oraQwery);
                    oraQwery.Close();
                    oraQwery.Open();
                    //logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                    //logger.Error(exp);
                }
            }
            if (parcedDocument.status == 2)
            {
                var cmdError = oraQwery.CreateCommand();

                cmdError.CommandText = $@"SELECT  LISTAGG(idl.short_log, ', ') WITHIN GROUP(ORDER BY idl.id) FROM iris_diadoc_log idl WHERE LOG_DATE = (SELECT MAX(LOG_DATE) FROM iris_diadoc_log idl2 WHERE diadoc_doc_id = {idDoc})  AND diadoc_doc_id = {idDoc}";

                parcedDocument.errMsg = cmdError.ExecuteScalar().ToString();

                _status = $"{parcedDocument.NameFile} - {parcedDocument.errMsg}";
            }
            if (_status == "")
            {
                parcedDocument.errMsg = "Ошибок при парсинге не найдено";
            }
            else
                parcedDocument.errMsg = _status;

            return parcedDocument;
        }


        public List<ParcedDocument> FillAllDoc()
        {
            List<ParcedDocument> ListParceDoc = new List<ParcedDocument>();
            string _status = "";
            ParcedDocument parcedDocument = new ParcedDocument();
            
            var cmdSelectDoc = oraQwery.CreateCommand();
            cmdSelectDoc.CommandText = @" SELECT status,email,cnt_tag, RU_APPROVE,entity_id,message_id, print_file_name,document,id,insertdate,print_file_name,'' error FROM iris_diadoc_doc  where  insertdate>='01.01.2022' ";
            using (cmdSelectDoc)
            {
                try
                {
                    cmdSelectDoc.Connection = oraQwery;
                    cmdSelectDoc.BindByName = true;
                    // cmdSelectDoc.Parameters.Add("id", idDoc);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmdSelectDoc))
                    {
                        DataSet dataSetResult = new DataSet();
                        da.Fill(dataSetResult);
                        //DataTable dt = dataSetResult.Tables[0];
                        foreach (DataTable table in dataSetResult.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {

                               

                                parcedDocument = new ParcedDocument
                                {
                                    Ids = (long)row[8],
                                    cntTag = row[2].ToString(),
                                    email = row[1].ToString(),
                                    entityId = row[4].ToString(),
                                    status = int.Parse(row[0].ToString()),
                                        isArrove = byte.Parse(row[3].ToString()),
                                        NameFile = row[6].ToString(),
                                        statusParce = "",
                                        statusParcePrint = "",
                                        errMsg = "",
                                        DocumentIn = row[7].ToString(),
                                        InsertDateDoc = (DateTime)row[9],
                                        messageId = row[5].ToString(),
                                      //  DocName = (string)cells[10]//row["ShortName"].ToString(),


                                };
                                    ListParceDoc.Add(parcedDocument);
                                //}
                            }
                        }
                    }
                }
                catch (OracleException e)
                {
                    //logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                    //logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(oraQwery);
                    oraQwery.Close();
                    oraQwery.Open();
                    //logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                    //logger.Error(exp);
                }
                return ListParceDoc;
            }
        }      
    }
}

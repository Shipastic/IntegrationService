using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using EDMIrisRetail.Controller;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EDMIrisRetail.Controller
{
    public class DocumentIrisController: IDocumentIrisIn, IDocumentIrisOut
    {
        OracleConnection oracleConnection = OracleConnectionState.GetInstance();

        List<DocumentIrisIn> GetDocumentIrises = new List<DocumentIrisIn>();

        List<DocumentIrisOut> GetDocumentIrisOuts = new List<DocumentIrisOut>();

        /// <summary>
        /// Метод проверки наличия документа в бд
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public bool DocumentExist(string messageId, string entityId)
        {
            var cmdCount = oracleConnection.CreateCommand();

            cmdCount.CommandText = $@"SELECT count(1) FROM iris_diadoc_doc WHERE entity_id='{entityId}' AND  message_id = '{messageId}' AND status not in (3, 4)";

            using (cmdCount)
            {
                string queryResString = cmdCount.ExecuteScalar().ToString();

                var queryResInt = Int16.Parse(queryResString);

                //Если количество строк, которые вернул запрос, >0 то значит такой документ уже есть и возвращаем TRUE
                if (queryResInt > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Метод для проверки наличия документа в таблице IRIS_INBOUND_DOC_DIADOC
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public bool DocumentAllExist(string messageId, string entityId)
        {
            var cmdCount = oracleConnection.CreateCommand();

            cmdCount.CommandText = $@"SELECT count(1) FROM IRIS_INBOUND_DOC_DIADOC WHERE entity_id='{entityId}' AND  message_id = '{messageId}'";

            using (cmdCount)
            {
                string queryResString = cmdCount.ExecuteScalar().ToString();

                var queryResInt = Int16.Parse(queryResString);

                //Если количество строк, которые вернул запрос, >0 то значит такой документ уже есть и возвращаем TRUE
                if (queryResInt > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Метод проверки документа в бд со статусом 6 и ру 1
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public bool DocExistWithRUAPPROVEStatus1(string messageID, string entityID)
        {
            var cmdCountRU = oracleConnection.CreateCommand();

            cmdCountRU.CommandText = $@"SELECT count(1) FROM iris_diadoc_doc WHERE entity_id='{entityID}' AND message_id = '{messageID}' AND status = 6 AND RU_APPROVE=1";

            using (cmdCountRU)
            {
                string queryResultStr = cmdCountRU.ExecuteScalar().ToString();

                var queryRes = Int16.Parse(queryResultStr);

                // Если запрос вернул хоть одну строку(документ с данными messageID и entityID уже существует базе)
                if (queryRes > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Метод проверки документа в бд со статусом 6 и ру 2
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public bool DocExistWithRUAPPROVEStatus2(string messageId, string entityId)
        {
            var cmdCountRU = oracleConnection.CreateCommand();

            cmdCountRU.CommandText = $@"SELECT count(1) FROM iris_diadoc_doc WHERE entity_id='{entityId}' AND message_id = '{messageId}' AND status = 6 AND RU_APPROVE=2";

            using (cmdCountRU)
            {
                string queryResultStr = cmdCountRU.ExecuteScalar().ToString();

                var queryRes = Int16.Parse(queryResultStr);

                // Если запрос вернул хоть одну строку(документ с данными messageID и entityID уже существует базе)
                if (queryRes > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Метод для фрмирования ИД для новых документов в таблице
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public long GetId(string tableName)
        {
            var cmdCreateId = oracleConnection.CreateCommand();
            cmdCreateId.CommandText = $"SELECT SQ_{tableName}.NEXTVAL FROM DUAL";
            using (cmdCreateId)
            {
                try
                {
                    //  cmd.Connection = this["FNP"];
                    cmdCreateId.BindByName = true;
                    var ret = cmdCreateId.ExecuteScalar().ToString();
                    return Int64.Parse(ret);
                }
                catch (OracleException e)
                {
                    // logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                    // logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(oracleConnection);
                    oracleConnection.Close();
                    oracleConnection.Open();
                    //  logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                    // logger.Error(exp);
                }
            }
            return 0;
        }

        public int GetIdRuStatus1(string messageID, string entityID)
        {
            var cmdGetId = oracleConnection.CreateCommand();

            cmdGetId.CommandText = $@"SELECT id FROM iris_diadoc_doc WHERE entity_id='{entityID}' AND message_id = '{messageID}' AND status = 6 AND RU_APPROVE=1";

            using (cmdGetId)
            {
                string queryRes = cmdGetId.ExecuteScalar().ToString();

                var resId = Int32.Parse(queryRes);

                return resId;
            }
        }

        /// <summary>
        /// Метод выбора ИД документа со статусом 6 и ру 2
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public int GetIdRuStatus2(string messageId, string entityId)
        {
            var cmdGetId = oracleConnection.CreateCommand();

            cmdGetId.CommandText = $@"SELECT id FROM iris_diadoc_doc WHERE entity_id='{entityId}' AND message_id = '{messageId}' AND status = 6 AND RU_APPROVE=2";

            using (cmdGetId)
            {
                string queryRes = cmdGetId.ExecuteScalar().ToString();

                var resId = Int32.Parse(queryRes);

                return resId;
            }
        }

        /// <summary>
        /// Метод для вставки всех документов в таблицу IRIS_INBOUND_DOC_DIADOC
        /// </summary>
        /// <param name="idDoc"></param>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        public void SetDocumentAll(long idDoc, string messageId, string entityId)
        {
            var cmdInsert = oracleConnection.CreateCommand();

            cmdInsert.CommandText = $@"INSERT INTO IRIS_INBOUND_DOC_DIADOC(id,message_id,entity_id) VALUES({idDoc},'{messageId}','{entityId}')";

            using (cmdInsert)
            {
                cmdInsert.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Метод добавления нового документа в бд
        /// </summary>
        /// <param name="contractor"></param>
        /// <param name="lastIndex"></param>
        /// <param name="document"></param>
        /// <param name="idDoc"></param>
        /// <param name="messageId"></param>
        /// <param name="entityId"></param>
        public void setDocument(Contractor contractor, string lastIndex, string document, long idDoc, string messageId, string entityId)
        {
            var cmdInsert = oracleConnection.CreateCommand();

            cmdInsert.CommandText = @"INSERT INTO iris_diadoc_doc (id,cnt_id,doc_type,last_Index,document,INSERTDATE,message_id,entity_id) VALUES ( :id,:cnt_id,:doc_type,:last_Index,:document,:dt,:message_id,:entity_id)";

            using (cmdInsert)
            {
                try
                {
                    cmdInsert.Connection = oracleConnection;
                    cmdInsert.BindByName = true;
                    cmdInsert.Parameters.Add("id", idDoc);
                    cmdInsert.Parameters.Add("cnt_id", contractor.Id);
                    cmdInsert.Parameters.Add("doc_type", "UTD");
                    cmdInsert.Parameters.Add("last_Index", lastIndex);
                    cmdInsert.Parameters.Add("document", document);
                    cmdInsert.Parameters.Add("dt", DateTime.Now);
                    cmdInsert.Parameters.Add("message_id", messageId);
                    cmdInsert.Parameters.Add("entity_id", entityId);
                    cmdInsert.ExecuteNonQuery();
                }
                catch (OracleException e)
                {

                    OracleConnection.ClearPool(oracleConnection);
                    oracleConnection.Close();
                    oracleConnection.Open();

                }
                catch (Exception exp)
                {

                }
            }

            var cmdCheck = oracleConnection.CreateCommand();

            ///Проверка документа в бд
            cmdCheck.CommandText = @"BEGIN iris_diadoc_pck.prc_check_diadoc_doc(:diadoc_doc_id); END;";

            using (cmdCheck)
            {
                try
                {
                    cmdCheck.Connection = oracleConnection;
                    cmdCheck.BindByName = true;
                    cmdCheck.Parameters.Add("diadoc_doc_id", idDoc);
                    cmdCheck.ExecuteNonQuery();
                }
                catch (OracleException e)
                {
                    OracleConnection.ClearPool(oracleConnection);
                    oracleConnection.Close();
                    oracleConnection.Open();
                }
                catch (Exception exp)
                {
                }
            }
            return;
        }

        /// <summary>
        /// Метод для повторной обработки документов со статусом 3
        /// </summary>
        /// <param name="cntId">ИД контрагента </param>
        /// <returns></returns>
        public List<DocReload> GetDocReloads(Int64 cntId)
        {
            DataSet set = new DataSet();
            List<DocReload> docs = new List<DocReload>();
            var cmdSelectDocReload = oracleConnection.CreateCommand();
            cmdSelectDocReload.CommandText = $@"SELECT id,message_id,entity_id FROM iris_diadoc_doc WHERE cnt_id={cntId} AND status=3 ORDER BY id";
            using (cmdSelectDocReload)
            {
                try
                {
                    cmdSelectDocReload.Connection = oracleConnection;
                    cmdSelectDocReload.BindByName = true;
                    using (OracleDataAdapter da = new OracleDataAdapter(cmdSelectDocReload))
                    {
                        da.Fill(set);
                        foreach (DataRow row in set.Tables[0].Rows)
                        {
                            DocReload docReload = new DocReload();
                            docReload.Id = Int64.Parse(row["ID"].ToString());
                            docReload.messageID = row["message_id"].ToString();
                            docReload.entityID = row["entity_id"].ToString();
                            docs.Add(docReload);
                        }
                    }
                }

                catch (OracleException e)
                {
                   // logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                   // logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(oracleConnection);
                    oracleConnection.Close();
                    oracleConnection.Open();
                   // logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                   // logger.Error(exp);
                }
            }
            return docs;
        }

        public DocumentList GetDocumentEDOs(DocumentsFilterIris documentsFilter)
        {        
            var documentList = EDMClass.apiDiadoc.GetDocuments(EDMClass.authTokenByLogin, documentsFilter);

            return documentList;
        }

        public DocumentList GetDocInbountReload(List<DocReload> docReloads, DocumentList list)
        {
            foreach (DocReload doc in docReloads)
            {
                if (!list.Documents.Any(m => m.MessageId == doc.messageID && m.EntityId == doc.entityID))
                {
                    var docum = EDMClass.apiDiadoc.GetDocument(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, doc.messageID, doc.entityID);

                    list.Documents.Add(docum);
                }
            }
            return list;
        }

        public List<Document> GetListCounteragents(DocumentList documentList)
        {
            List<Document> _documentList = documentList.Documents.Where(d => d.CustomDocumentId == ""
                                                         || d.CounteragentBoxId == "2bf2f29c850b4102aca775cbbaf047e1@diadoc.ru"  //"ООО "КОМПАНИЯ ПРОФИЛАЙН"
                                                         || d.CounteragentBoxId == "8f8d105f46cc449090e85597d898a30e@diadoc.ru"  //О-Си-Эс Центр
                                                         || d.CounteragentBoxId == "74cff9fcc92c45569de1f4bb29c17a5f@diadoc.ru"  //ОПЕН КОМПЬЮТЕР СОЛЮШН
                                                         || d.CounteragentBoxId == "8ca1ca1cb15c4c3a9221b88a123b923d@diadoc.ru"  //Компания РМ (расходные материалы)
                                                         || d.CounteragentBoxId == "d3cc9a4cb7f64c83be24dbee604866c5@diadoc.ru"  // Луис+
                                                         || d.CounteragentBoxId == "eda9e0da8e4a4b00af72f5ff98062303@diadoc.ru"  // ИНИЦИАТИВА
                                                         || d.CounteragentBoxId == "505280e928ee47ad8ac27141d11da6e6@diadoc.ru"  // АЦИС Технология
                                                         || d.CounteragentBoxId == "0f5b1d6620394e459715fdcbc1464d24@diadoc.ru"
                                                         || d.CounteragentBoxId == "64baa3fa02f646b9963407341560282d@diadoc.ru").ToList();
            return _documentList;
        }


        /// <summary>
        /// Метод для получения всех входящих УПД
        /// </summary>
        /// <param name="contractors"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public List<DocumentIrisIn> GetDocumentInbIris(List<Contractor> contractors, List<DepartmentIris> departments)
        {
            DocumentFilterIrisController filterIrisContraller = new DocumentFilterIrisController();

            foreach (Contractor contr in contractors)
            {
                var filterForDiadoc = filterIrisContraller.SetFilterCurrContractorInboundDiadoc(Constants.DefaultFromBoxId, Constants.filterCategoryDocumentAIIIn, contr.BoxId, filterIrisContraller.getDate(contr));

                var documentList = EDMClass.apiDiadoc.GetDocuments(EDMClass.authTokenByLogin, filterForDiadoc);

                foreach (var item in documentList.Documents)
                {
                    var tagName = from d in departments
                                  join docList in documentList.Documents on
                                  d.ParentDepartmentIdIris equals docList.ToDepartmentId
                                  select d.AbbreviationIris;

                    var curTag = tagName.FirstOrDefault();
                    DocumentIrisIn documentIris = new DocumentIrisIn
                    {
                        Title      = item.Title,
                        Status     = item.DocflowStatus.PrimaryStatus.StatusText,
                        FromDep    = contr.Name,
                        EntityID   = item.EntityId,
                        MessageID  = item.MessageId,
                        CreateDate = item.CreationTimestamp,
                        ShortName  = curTag,
                        ToDep      = item.DocflowStatus.SecondaryStatus?.StatusText ?? "Статус неизвестен"
                    };
                    GetDocumentIrises.Add(documentIris);
                }
            }           
            return GetDocumentIrises;
        }

        
        /// <summary>
        /// Метод для получения всех входящих исправлений СФ
        /// </summary>
        /// <param name="counteragentLists"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public List<DocumentIrisIn> GetDocumentInSFbIris(List<Counteragent> counteragentLists, List<DepartmentIris> departments)
        {
            DocumentFilterIrisController filterIrisContraller = new DocumentFilterIrisController();

            foreach (var contr in counteragentLists.OrderBy(n => n.Organization.ShortName))
            {
                var filterForDiadoc = filterIrisContraller.SetFilterCurrContractorInboundDiadoc(Constants.DefaultFromBoxId, Constants.filterSF, contr.Organization.Boxes.Select(b => b.BoxId).FirstOrDefault(), DateTime.Now.AddDays(-60));

                var documentList = EDMClass.apiDiadoc.GetDocuments(EDMClass.authTokenByLogin, filterForDiadoc);

                foreach (var item in documentList.Documents)
                {
                    var tagName = from d in departments
                                  join docList in documentList.Documents on
                                  d.ParentDepartmentIdIris equals docList.ToDepartmentId
                                  select d.AbbreviationIris;

                    var curTag = tagName.FirstOrDefault();
                    DocumentIrisIn documentIris = new DocumentIrisIn
                    {
                        Title = item.Title,
                        Status = item.DocflowStatus.PrimaryStatus.StatusText,
                        FromDep = contr.Organization.ShortName,
                        EntityID = item.EntityId,
                        MessageID = item.MessageId,
                        CreateDate = item.CreationTimestamp,
                        ShortName = curTag,
                        ToDep = item.DocflowStatus.SecondaryStatus?.StatusText ?? "Статус неизвестен"
                    };
                    GetDocumentIrises.Add(documentIris);
                }
            }
            return GetDocumentIrises;
        }


        /// <summary>
        /// Метод для получения всех корректировок СФ
        /// </summary>
        /// <param name="counteragentLists"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public List<DocumentIrisIn> GetDocumentInSFCorIris(List<Counteragent> counteragentLists, List<DepartmentIris> departments)
        {
            DocumentFilterIrisController filterIrisContraller = new DocumentFilterIrisController();

            foreach (var contr in counteragentLists.OrderBy(n => n.Organization.ShortName))
            {

                var filterForDiadoc = filterIrisContraller.SetFilterCurrContractorInboundDiadoc(Constants.DefaultFromBoxId, Constants.filterSFCorrection, contr.Organization.Boxes.Select(b => b.BoxId).FirstOrDefault(), DateTime.Now.AddDays(-600));

                var documentList = EDMClass.apiDiadoc.GetDocuments(EDMClass.authTokenByLogin, filterForDiadoc);

                foreach (var item in documentList.Documents)
                {
                    var tagName = from d in departments
                                  join docList in documentList.Documents on
                                  d.ParentDepartmentIdIris equals docList.ToDepartmentId
                                  select d.AbbreviationIris;

                    var curTag = tagName.FirstOrDefault();
                    DocumentIrisIn documentIris = new DocumentIrisIn
                    {
                        Title = item.Title,
                        Status = item.DocflowStatus.PrimaryStatus.StatusText,
                        FromDep = contr.Organization.ShortName,
                        EntityID = item.EntityId,
                        MessageID = item.MessageId,
                        CreateDate = item.CreationTimestamp,
                        ShortName = curTag,
                        ToDep = item.DocflowStatus.SecondaryStatus?.StatusText ?? "Статус неизвестен"
                    };
                    GetDocumentIrises.Add(documentIris);
                }
            }
            return GetDocumentIrises;
        }


        /// <summary>
        /// Метод для получения исходящих документов
        /// </summary>
        /// <param name="contractors"></param>
        /// <param name="departments"></param>
        /// <returns></returns>
        public List<DocumentIrisOut> GetDocumentOutbIris(List<Contractor> contractors, List<DepartmentIris> departments)
        {
            DocumentFilterIrisController filterIrisContraller = new DocumentFilterIrisController();

            UserIrisController userIrisController = new UserIrisController();
            foreach (Contractor contr in contractors)
            {
                var filterForDiadoc = filterIrisContraller.SetFilterCurrContractorInboundDiadoc(Constants.DefaultFromBoxId, Constants.filterCategoryDocumentAIIOut, contr.BoxId, filterIrisContraller.getDate(contr));

                var documentList = EDMClass.apiDiadoc.GetDocuments(EDMClass.authTokenByLogin, filterForDiadoc);

                foreach (var item in documentList.Documents)
                {
                    var tagName = from d in departments
                                  join docList in documentList.Documents on
                                  d.ParentDepartmentIdIris equals docList.ToDepartmentId
                                  select d.AbbreviationIris;
                    var curTag = tagName.FirstOrDefault();
                    DocumentIrisOut documentIris = new DocumentIrisOut
                    {
                        Title      = item.Title,
                        Status     = item.DocflowStatus.PrimaryStatus.StatusText,
                        FromDep    = contr.Name,
                        EntityID   = item.EntityId,
                        MessageID  = item.MessageId,
                        CreateDate = item.CreationTimestamp,
                        ShortName  = curTag,
                        ToDep      = item.DocflowStatus.SecondaryStatus?.StatusText ?? "Статус неизвестен"
                    };
                    GetDocumentIrisOuts.Add(documentIris);
                }
            }
            return GetDocumentIrisOuts;
        }
    }
}

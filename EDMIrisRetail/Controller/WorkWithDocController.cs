using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Events;
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
    public class WorkWithDocController: IWorkWithDoc, IStateDocument
    {
        ContractorController contractorController = new ContractorController();

        UserIrisController userIrisController = new UserIrisController();

        List<Employee> employees = new List<Employee>();

        List<Employee> ListUserIris = new List<Employee>();

        DepartmentIrisController departmentIrisController = new DepartmentIrisController();

        List<Contractor> ListContractor = new List<Contractor>();

        List<Diadoc.Api.Proto.Departments.Department> ListDepart = new List<Diadoc.Api.Proto.Departments.Department>();

        DocumentFilterIrisController documentFilterIrisController = new DocumentFilterIrisController();

        DocumentsFilterIris documentFilterIrisParam;

        DocumentsFilterIris documWithSign;

        DocumentIrisController documentListIris = new DocumentIrisController();

        DocumentIrisController documentWithSig = new DocumentIrisController();

        List<WorkWithDoc> ListDoc = new List<WorkWithDoc>();

        List<WorkWithDocNew> ListDocNew = new List<WorkWithDocNew>();

        WorkWithDocSign workWithDocSign = new WorkWithDocSign();

        WorkWithDocSign withDocSign = new WorkWithDocSign();

        WorkWithDocSign WithDocSign;      

        WorkWithDocNew docNew = new WorkWithDocNew();

        List<WorkWithDocSign> ListDocSign = new List<WorkWithDocSign>();

        List<WorkWithDocSign> docSigns = new List<WorkWithDocSign>();

        MessageIris messageIris = new MessageIris();

        ContentDocumentController contentDocumentController = new ContentDocumentController();

        ParcedDocument ResultParce;

        ParcedDocument ResultParceWithSign;

        ParcedDocumentController parcedDocument_ = new ParcedDocumentController();

        ParcedDocument parcedDocument = new ParcedDocument();

        ResolutionType resolutionType;

        Facade facade = new Facade();

        OracleConnection connectionState = OracleConnectionState.GetInstance();

        /// <summary>
        /// Счетчик сущностей в сообщении
        /// </summary>
        int maxLabel = 1;

        /// <summary>
        /// Счетчик поставщиков
        /// </summary>
        int step = 1;
        int step2 = 1;
        /// <summary>
        /// Строка для комментария
        /// </summary>
        string msg = "";

        /// <summary>
        /// Строка для комментария
        /// </summary>
        string msg2 = "";

        /// <summary>
        /// Название документа у поставщика
        /// </summary>
        string DocumentNameContr = "";

        DateTime curDate = DateTime.Now;

        DateTime curDateForSign = DateTime.Now;

        public List<WorkWithDoc> GetConnect()
        {
            ListContractor = contractorController.GetContractors();

            ListUserIris = userIrisController.GetuserIris(employees);

            ListDepart = departmentIrisController.GetDepartmentsIris();

            CheckDocuments();

            ListDoc = GetListContractors(ListContractor, ListUserIris, ListDepart);

            return ListDoc;

        }

        #region новые методы для проверки реквизитов отправителя
        /// <summary>
        /// Метод для проверки документов УПД и СФ
        /// </summary>
        public void CheckDocuments()
        {
            int docAllExist = 0;

            ResolutionType resolutionTypeAllDoc;

            List<Counteragent> counteragents = new List<Counteragent>();

            ContractorController contractorControllerCheck = new ContractorController();

            DocumentIrisController documentListAll = new DocumentIrisController();
            
            DocumentsFilterIris documentFilterIrisParamCheck;
          
            MessageIris messageCounteragents = new MessageIris();

            DocumentsFilterIris docFilterWithSender;

            DocumentsFilterIris docFilterWithRecipient;

            DocumentIrisController documentListSender = new DocumentIrisController();

            DocumentIrisController documentListRecipient = new DocumentIrisController();

            counteragents = contractorControllerCheck.GetCounteragentLists();

            foreach (Counteragent counteragent in counteragents)
            {
                //Строка с результатом проверок
                string resultCheck = "";

                ///Фильтр для всех входящих документов
                documentFilterIrisParamCheck = new DocumentsFilterIris(Constants.DefaultFromBoxId, counteragent.Organization.Boxes.Select(b => b.BoxId).FirstOrDefault(), Constants.filterCategoryDocumentAII, DateTime.Now.AddDays(-4));          

                var ListDoc = documentListAll.GetDocumentEDOs(documentFilterIrisParamCheck);

                foreach (var item in ListDoc.Documents)
                {
                   
                    EDMClass eDMClass = new EDMClass();

                    MessageToPostIris messageTo = new MessageToPostIris();

                    RequisitesDocumentSender requisitesSender = new RequisitesDocumentSender();

                    MessagePathToPostIris toPost = new MessagePathToPostIris();

                    RequisitesDocumentRecipient requisitesRecipient = new RequisitesDocumentRecipient();

                    RessolutionAttachmentClass attachResol = new RessolutionAttachmentClass();

                    RequisitesDocumentSenderController requisitesDocumentSenderController = new RequisitesDocumentSenderController();

                    RequisitesDocumentRecipientController requisitesDocumentRecipientController = new RequisitesDocumentRecipientController();

                    RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTSender = new RequisitesDocumentFromQNTSOFT();

                    RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTRecipient = new RequisitesDocumentFromQNTSOFT();

                    RequisitesDocumentFromQNTSOFTController requisitesDocumentFromQNTSOFTSenderController = new RequisitesDocumentFromQNTSOFTController();

                    RequisitesDocumentFromQNTSOFTController requisitesDocumentFromQNTSOFTRecipientController = new RequisitesDocumentFromQNTSOFTController();

                    
                    ///Получаем все вложения сообщения
                    var mesContant = messageCounteragents.GetMessageED(item);

                    ///Выбор времени подписания получателем
                    var dateSignedRecipient = mesContant.Entities                                        
                                         .Where(c => c.EntityType.Equals(EntityType.Signature))
                                         .Where(b => b.SignerBoxId.Equals(Constants.DefaultFromBoxId))
                                         .Select(t => t.CreationTime)
                                         .FirstOrDefault();

                    ///Выбор времени подписания отправителем
                    var dateSignedSender = mesContant.Entities
                                                     .Where(c => c.EntityType.Equals(EntityType.Signature))
                                                     .Where(b => b.SignerBoxId.Equals(counteragent.Organization.Boxes.Select(f => f.BoxId).FirstOrDefault()))
                                                     .Select(t => t.CreationTime)
                                                     .FirstOrDefault();

                    ///Получаем все содержимое из всей цепочки сообщений для УПД
                    var contantMessage = contentDocumentController.GetContentDocuments(mesContant, maxLabel);

                    ///Получаем все содержимое из всей цепочки сообщений для СФ
                    var contantMessageSF = contentDocumentController.GetContentDocumentsSF(mesContant, maxLabel);

                    //установка реквизитов документа объекту, исходя из типа документа
                    ///Если документ - УПД
                    if (contantMessage.Count != 0)
                    {
                        //Проверка наличия документа в таблице IRIS_INBOUND_DOC_DIADOC
                        if (documentListAll.DocumentAllExist(item.MessageId, item.EntityId) == false)
                        {
                            docAllExist = 1;

                            //Генерируем ID для строки
                            Int64 idDoc = documentListAll.GetId("IRIS_INBOUND_DOC_DIADOC");

                            documentListAll.SetDocumentAll(idDoc, item.MessageId, item.EntityId);
                        }
                        else
                        {
                            docAllExist = 0;

                            break;
                        }

                        ///Получение реквизитов отправителя
                        requisitesSender = requisitesDocumentSenderController.GetDataFromDocumentSender(contantMessage, item, mesContant);

                        ///Получение реквизитов получателя
                        requisitesRecipient = requisitesDocumentRecipientController.GetDataFromDocumentRecipient(contantMessage, item, mesContant);

                        ///Проверка даты подписания
                        resultCheck += CheckDateSigned(item, mesContant, counteragent, docAllExist);

                        ///Результат проверки реквизитов отправителя
                        resultCheck += CheckSender(requisitesSender, requisitesDocumentFromQNTSOFTSenderController, requisitesDocumentFromQNTSOFTSender);

                        ///Результат проверки реквизитов ролучателя
                        resultCheck += CheckRecipient(requisitesRecipient, requisitesDocumentFromQNTSOFTRecipientController, requisitesDocumentFromQNTSOFTRecipient);

                        resolutionTypeAllDoc = ResolutionType.Approve;

                        var resAttach = attachResol.SetResolutionAttach(resolutionTypeAllDoc, resultCheck, item.EntityId);

                        var messagePathToPost = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                        MessagePatchToPost post = new MessagePatchToPost();

                        messagePathToPost.AddResolution(resAttach);

                        eDMClass.PostMessagePatch(messagePathToPost);
                    }
                    else
                    ///Если документ-СФ
                    if (contantMessageSF.Count != 0)
                    {

                        //Проверка наличия документа в таблице IRIS_INBOUND_DOC_DIADOC
                        if (documentListAll.DocumentAllExist(item.MessageId, item.EntityId) == false)
                        {
                            docAllExist = 1;

                            //Генерируем ID для строки
                            Int64 idDoc = documentListAll.GetId("IRIS_INBOUND_DOC_DIADOC");

                            documentListAll.SetDocumentAll(idDoc, item.MessageId, item.EntityId);
                        }
                        else
                        {
                            docAllExist = 0;
                            break;
                        }
                        ///Получение реквизитов отправителя
                        requisitesSender = requisitesDocumentSenderController.GetDataFromDocumentSender(contantMessageSF, item, mesContant);

                        ///Получение реквизитов получателя
                        requisitesRecipient = requisitesDocumentRecipientController.GetDataFromDocumentRecipient(contantMessageSF, item, mesContant);

                        resultCheck += CheckDateSigned(item, mesContant, counteragent, docAllExist);

                        ///Результат проверки реквизитов отправителя
                        resultCheck += CheckSender(requisitesSender, requisitesDocumentFromQNTSOFTSenderController, requisitesDocumentFromQNTSOFTSender);

                        ///Результат проверки реквизитов ролучателя
                        resultCheck += CheckRecipient(requisitesRecipient, requisitesDocumentFromQNTSOFTRecipientController, requisitesDocumentFromQNTSOFTRecipient);

                        resolutionTypeAllDoc = ResolutionType.Approve;

                        var resAttach = attachResol.SetResolutionAttach(resolutionTypeAllDoc, resultCheck, item.EntityId);

                        var messageToPost = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                        messageToPost.AddResolution(resAttach);

                        eDMClass.PostMessagePatch(messageToPost);
                    }

                }
            }
        }

        /// <summary>
        /// Метод проверки даты подписания документа входящих документов
        /// </summary>
        /// <param name="counteragent"></param>
        /// <returns></returns>
        private string CheckDateSigned(Document document, Message message, Counteragent counteragent, int flagCheckDocWithTable)
        {
            string chekDocSignDate = "";

            DateTime creationDate = document.CreationTimestamp;

            ///Если документ новый и отсутствует в бд
            ///То проверяем только дату подписания отправителем
            if (flagCheckDocWithTable == 1)
            {
                ///Выбор времени подписания отправителем
                DateTime dateSignedSender = message.Entities
                                                 .Where(c => c.EntityType.Equals(EntityType.Signature))
                                                 .Where(b => b.SignerBoxId.Equals(counteragent.Organization.Boxes.Select(f => f.BoxId).FirstOrDefault()))
                                                 .Select(t => t.CreationTime)
                                                 .FirstOrDefault();

                if (dateSignedSender > creationDate)
                {
                    chekDocSignDate += "Дата подписания документа отправителем не ранее даты добавления документа";
                }
                else
                {
                    chekDocSignDate += $"Дата подписания документа отправителем: {dateSignedSender} раньше даты добавления документа: {creationDate}\n";
                }

            }
            ///Иначе проверяем дату подписания получателем
            else
            {
                ///Выбор времени подписания получателем
                DateTime dateSignedRecipient = message.Entities
                                     .Where(c => c.EntityType.Equals(EntityType.Signature))
                                     .Where(b => b.SignerBoxId.Equals(Constants.DefaultFromBoxId))
                                     .Select(t => t.CreationTime)
                                     .FirstOrDefault();

                if (dateSignedRecipient > creationDate)
                {
                    chekDocSignDate += "Дата подписания документа отправителем не ранее даты добавления документа";
                }
                else
                {
                    chekDocSignDate += $"Дата подписания документа отправителем: {dateSignedRecipient} раньше даты добавления документа: {creationDate}\n";
                }
            }
            return chekDocSignDate;
        }
        #endregion

        public List<WorkWithDoc> GetListContractors(List<Contractor> _ListContractor, List<Employee> _LisiEmp, List<Diadoc.Api.Proto.Departments.Department> _ListDep)
        {
            List<WorkWithDoc> withDocs = new List<WorkWithDoc>();

            WorkWithDoc _work = new WorkWithDoc();

            WorkWithDoc __work = new WorkWithDoc();

            _ListContractor = ListContractor;

            _LisiEmp = ListUserIris;

            _ListDep = ListDepart;

            ///Для каждого поставщика выбираем документы согласно фильтра
            foreach (Contractor contr in _ListContractor)
            {

                ///Устанавливаем интервал дней за который будем выгружать документы из бд
                curDate = documentFilterIrisController.getDate(contr);

                ///Устанавливаем интервал дней за который будем выгружать документы с подписью из бд
                curDateForSign = documentFilterIrisController.getDateForSigned(contr);

                ///Задаем фильтр для выгрузки входящих документов с незавершенным документооборотом
                documentFilterIrisParam = new DocumentsFilterIris(Constants.DefaultFromBoxId, contr.BoxId, Constants.filterCategoryDocumentAIIIn, curDate, contr);

                //Тест
                //documentFilterIrisParam = new DocumentsFilterIris(Constants.DefaultFromBoxId, contr.BoxId, Constants.filterCategoryDocumentAIIIn, curDate, contr);

                ///Получаем список документов
                Document testDoc = new Document();
                Document testDocNew = new Document();

                var ListDoc = documentListIris.GetDocumentEDOs(documentFilterIrisParam);

                //testDocNew = testDoc;
                ///Задаем фильтр для выгрузки входящих документов с подписью
                documWithSign = new DocumentsFilterIris(Constants.DefaultFromBoxId, contr.BoxId, Constants.filterCatDocumentWithRecSigned, curDateForSign, contr);

                ///Получаем список документов с подписью
                var ListDocWithRecSign = documentWithSig.GetDocumentEDOs(documWithSign);

                string messageId = null;

                string documentId = null;

                __work = SetCommentForDocWithParamFilter(contr, ListDoc, _work);


                ///Устанавливаем значения свойств дя объекта класса WorkWithDoc
                WorkWithDoc work = new WorkWithDoc
                {
                    CountContractor = ListContractor.Count,

                    CountUserIris = ListUserIris.Count,

                    CountDepartmentIris = ListDepart.Count,

                    DocforWork = __work.DocforWork,

                    DocumentName = __work?.DocumentName ?? "Документов для обработки не найдено!",

                    ListContragent = $"{step++} | {Constants.listContragents} - {contr.Name}\n"
                };

                withDocSign = SetCommentForDocWithSigned(contr, ListDocWithRecSign, workWithDocSign);

                ///Устанавливаем значения свойств дя объекта класса WorkWithDocSign
                WithDocSign = new WorkWithDocSign
                {
                    CountContractor = ListContractor.Count,

                    CountUserIris = ListUserIris.Count,

                    CountDepartmentIris = ListDepart.Count,

                    DocforWork = withDocSign.DocforWork,

                    DocumentName = withDocSign?.DocumentName ?? "Документов для обработки не найдено!",

                    ListContragent = $"{step2++} | {Constants.listContragents} - {contr.Name}\n"
                };

                ///Формируем список из объектов класса WorkWithDocSign
                ListDocSign.Add(WithDocSign);

                ///Метод для проброса списка документов с подписями во View
                GetDocSign(ListDocSign);

                ///Формируем список из объектов класса WorkWithDoc
                withDocs.Add(work);
                work.DocumentName = "";
                work.DocforWork = "";
                var documentList = documentListIris.GetListCounteragents(ListDoc);

                var docListWuthRecSigned = documentWithSig.GetListCounteragents(ListDocWithRecSign);

                foreach (var item in documentList)
                {
                    var mesContant = messageIris.GetMessageED(item);

                    ///Получаем все содержимое из всей цепочки сообщений для УПД
                    var contantMessage = contentDocumentController.GetContentDocuments(mesContant, maxLabel);

                    ParcedDocument parced = new ParcedDocument();                 

                    //проверка документа и его запись в бд
                    ResultParce = parcedDocument_.ExecParceDoc(contr, item, mesContant, contantMessage, parced);

                    if (ResultParce == null || ResultParce.Ids == 0)
                    { continue; }

                    if (ResultParce.statusParce == $"{ResultParce.NameFile } : { Constants.newdocForDB}")
                        {
                            docNew.ListContragent = contr.Name;

                            docNew.ResolutionStatus = ResultParce.statusParce;

                            docNew.ErrorMessage = ResultParce.errMsg;
                        }
                        else
                        {
                            work.ResolutionStatus = ResultParce.statusParce;
                            work.ErrorMessage = ResultParce.errMsg;
                        }

                        
                    ///Получаем пользователя для согласования документа
                    var curUser = userIrisController.GetId(ResultParce, ListUserIris);

                    msg = SetFirstStateDocument(ResultParce);

                        maxLabel++;

                        msg2 = SetSecondStatedocument(ResultParce);

                        string resCom = $"{ResultParce.NameFile} - {Constants.statusIAS}";

                        if (ResultParce.statusParce.Contains(resCom))
                        {
                            docNew.ResolutionStatus += resCom + $"в { DateTime.Now} { Constants.msgCommentStatus_1_1}";
                        }
                        else
                        {
                            work.ResolutionStatus += ResultParce.statusParce;
                            work.ErrorMessage += ResultParce.errMsg;
                        }

                        facade.SendMessagePath(item, ResultParce, msg, msg2, maxLabel, mesContant, curUser, ListDepart, resolutionType, contr);

                        work.ResolutionStatus += ResultParce.statusParce;
                        docNew.ResolutionStatus += ResultParce.statusParce;
                        ListDocNew.Add(docNew);
                        GetDocNew(ListDocNew);
                }
                
                foreach (var docSign in docListWuthRecSigned)
                {
                    var mesContant = messageIris.GetMessageED(docSign);

                    var contantMessage = contentDocumentController.GetContentDocuments(mesContant, maxLabel);

                    ParcedDocument parced = new ParcedDocument();

                    ResultParceWithSign = parcedDocument_.ExecParceDocWithSign(contr, docSign, mesContant, contantMessage, parced );

                    if (ResultParceWithSign == null || ResultParceWithSign.Ids == 0)
                    {
                        continue;
                    }
                    else
                    {
                        OracleConnectionState.SetRuApproveStatus3(contr, mesContant.MessageId, docSign.EntityId);
                    }
                    if (messageId == null && documentId == null)
                    {
                        work.ResolutionStatus += $"{Constants.docNotExist}\n";
                    }
                }

            }
            step = 1;
            return withDocs;
        }

        public string SetSecondStatedocument(ParcedDocument resultParce)
        {
            string msgComment = "";

            if (resultParce.status == 1 && resultParce.isArrove == 0)
            {
                msgComment = Constants.msgCommentStatus_1_1;

                resultParce.statusParce += msgComment;
            }
            else
            {
                msgComment = resultParce.errMsg;

                if(msgComment is null)
                {
                    resultParce.errMsg = "Ошибок не найдено!\n\n";
                }
            }
            return msgComment;
        }

        /// <summary>
        /// Метод установки комментария для документа в зависимости от статуса и подтверждения от ру
        /// </summary>
        /// <param name="resultParce"> документ из бд </param>
        /// <returns></returns>
        public string SetFirstStateDocument(ParcedDocument resultParce)
        {
            string msgComment = "";

            if (resultParce.status == 1 && resultParce.isArrove == 0)
            {
                //TODO добавить  комментарий название документа
                msgComment = $"{resultParce.NameFile} - {Constants.statusIAS} в {DateTime.Now}";

                resultParce.statusParce = msgComment;

                resolutionType = ResolutionType.Approve;

            }
            else
            if (resultParce.status == 6 && resultParce.isArrove == 1)
            {
                msgComment = $"{resultParce.NameFile} - {Constants.docCapitalize} в {DateTime.Now}";

                resultParce.statusParce = msgComment;

                resolutionType = ResolutionType.Approve;
            }
            else
            {
                msgComment = $"{resultParce.NameFile} - {resultParce.errMsg} в {DateTime.Now}";

                resultParce.errMsg = msgComment;

                resolutionType = ResolutionType.Disapprove;
            }
            return msgComment;
        }

        //TODO - метод только для WPF
        public List<WorkWithDocSign> GetDocSign(List<WorkWithDocSign> listDocSign)
        {
            listDocSign = ListDocSign;

            return listDocSign;
        }

        //TODO - метод только для WPF
        public List<WorkWithDocNew> GetDocNew(List<WorkWithDocNew> listDocNew)
        {
            listDocNew = ListDocNew;

            return listDocNew;
        }

        /// <summary>
        /// Метод для установки статусов документов с подписью
        /// </summary>
        /// <param name="contr"> поставщик </param>
        /// <param name="listDocWithRecSign"> список подписанных документов </param>
        /// <param name="workWithDocSign"> объект для установки свойств </param>
        /// <returns></returns>
        private WorkWithDocSign SetCommentForDocWithSigned(Contractor contr, DocumentList listDocWithRecSign, WorkWithDocSign workWithDocSign)
        {
            if (listDocWithRecSign.TotalCount == 0)
            {
                workWithDocSign.DocforWork = $"Для {contr.Name} подписанные документы отсутствуют";
            }
            else
            {
                workWithDocSign.DocforWork = "Список подписанных документов:\n";

                foreach (var item in listDocWithRecSign.Documents.OrderBy(o => o.Title))
                {
                    DocumentNameContr += $"\rНазвание подписанного документа: {item.Title}\n";

                }

                workWithDocSign.DocumentName = DocumentNameContr;

                DocumentNameContr = "";
            }

            return workWithDocSign;
        }

        /// <summary>
        /// Метод для установки статусов документа
        /// </summary>
        /// <param name="contr"> поставщик </param>
        /// <param name="ListDoc"> список всех документов </param>
        /// <param name="_work"> объект для установки </param>
        /// <returns></returns>
        public WorkWithDoc SetCommentForDocWithParamFilter(Contractor contr, DocumentList ListDoc, WorkWithDoc _work)
        {
            if (ListDoc.TotalCount == 0)
            {
                _work.DocforWork = $"Для {contr.Name} документы, требующие обработки, отсутствуют";
            }
            else
            {
                _work.DocforWork = "\rСписок документов согласно фильтра:\n\n";

                foreach (var item in ListDoc.Documents.OrderBy(o => o.Title))
                {
                    DocumentNameContr += $"\rНазвание документа: {item.Title}\n";

                }

                _work.DocumentName += $"{DocumentNameContr}\n";

                DocumentNameContr = "";
            }

            return _work;
        }    
        
        /// <summary>
        /// Метод для сравнения реквизитов отправителя документа с данными из апи
        /// </summary>
        /// <param name="requisites"></param>
        /// <param name="requisitesDocumentFromQNTSOFTSender"></param>
        /// <returns></returns>
        public string EqualsRequisitesOrganizationSender(RequisitesDocumentSender requisites, RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTSender, string orgAddressDoc)
        {
            string resComent = "Проверка реквизитов отправителя\n";

           if (requisitesDocumentFromQNTSOFTSender.legal_entities[0].address.Equals(orgAddressDoc))
            {
                resComent += "Адрес организации указан верно\n";
            }
           else
            {
                resComent += $"Адрес организации отличается от адресса, указанного в реестре ЕГРЮЛ! \nпо документу: \"{orgAddressDoc}\"\nпо реестру: \"{requisitesDocumentFromQNTSOFTSender.legal_entities[0].address}\"\n";
            }

            if (requisitesDocumentFromQNTSOFTSender.legal_entities[0].inn.Equals(requisites.INN))
            {
                resComent += "ИНН организации указан верно\n";
            }
            else
            {
                resComent += $"ИНН организации:\"{requisites.INN}\"  не совпадает с ИНН из ЕГРЮЛ \"{requisitesDocumentFromQNTSOFTSender.legal_entities[0].inn}\"!\n";
            }

            var DocumentFromQNTSOFTName = requisitesDocumentFromQNTSOFTSender.GetNameShortOrg(requisitesDocumentFromQNTSOFTSender.legal_entities[0].opf_full, requisitesDocumentFromQNTSOFTSender.legal_entities[0].name_full);

            if(DocumentFromQNTSOFTName.Equals(requisites.NameOrg))
            {
                resComent += "Название организации указан верно\n";
            }
            else
            {
                resComent += $"Название организации отличается от названия, указанного в реестре ЕГРЮЛ! \nпо документу: \"{requisites.NameOrg}\"\nпо реестру: \"{DocumentFromQNTSOFTName}\"\n";
            }

            if (requisites.KPP.Equals(requisitesDocumentFromQNTSOFTSender.legal_entities[0].kpp))
            {
                resComent += "КПП организации указан верно\n\n";
            }
            else
            {
                resComent += $"КПП организации: \"{requisites.KPP}\" не совпадает с КПП из ЕГРЮЛ \"{requisitesDocumentFromQNTSOFTSender.legal_entities[0].kpp}\"!\n\n";
            }

            if(requisites.Currency != "643")
            {
                resComent += "Валюта не российский рубль!\n";
            }
            else
            {
                resComent += "Валюта - российский рубль\n\n";
            }    
            return resComent;
        }

        /// <summary>
        ///  Метод для сравнения реквизитов получателя документа с данными из апи
        /// </summary>
        /// <param name="requisites"></param>
        /// <param name="requisitesDocumentFromQNTSOFTRecipient"></param>
        /// <param name="orgAddressDoc"></param>
        /// <returns></returns>
        public string EqualsRequisitesOrganizationRecipient(RequisitesDocumentRecipient requisites, RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTRecipient, string orgAddressDoc)
        {
            string resComent = "Проверка реквизитов получателя\n";

            if (requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].address.Equals(orgAddressDoc))
            {
                resComent += "Адрес организации указан верно\n";
            }
            else
            {
                resComent += $"Адрес организации отличается от адресса, указанного в реестре ЕГРЮЛ! \nпо документу: \"{orgAddressDoc}\"\nпо реестру: \"{requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].address}\"\n";
            }

            if (requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].inn.Equals(requisites.INN))
            {
                resComent += "ИНН организации указан верно\n";
            }
            else
            {
                resComent += $"ИНН организации:\"{requisites.INN}\"  не совпадает с ИНН из ЕГРЮЛ \"{requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].inn}\"!\n";
            }

            var DocumentFromQNTSOFTName = requisitesDocumentFromQNTSOFTRecipient.GetNameShortOrg(requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].opf_full, requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].name_full);

            if (DocumentFromQNTSOFTName.Equals(requisites.NameOrg))
            {
                resComent += "Название организации указан верно\n";
            }
            else
            {
                resComent += $"Название организации отличается от названия, указанного в реестре ЕГРЮЛ! \nпо документу: \"{requisites.NameOrg}\"\nпо реестру: \"{DocumentFromQNTSOFTName}\"\n";
            }

            if (requisites.KPP.Equals(requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].kpp))
            {
                resComent += "КПП организации указан верно\n\n";
            }
            else
            {
                resComent += $"КПП организации: \"{requisites.KPP}\" не совпадает с КПП из ЕГРЮЛ \"{requisitesDocumentFromQNTSOFTRecipient.legal_entities[0].kpp}\"!\n\n";
            }         

            return resComent;
        }

        /// <summary>
        /// Метод для проверки реквизитов отправителя
        /// </summary>
        /// <param name="requisitesSender"></param>
        /// <param name="requisitesDocumentFromQNTSOFTSenderController"></param>
        /// <param name="requisitesDocumentFromQNTSOFTSender"></param>
        /// <returns></returns>
        public string CheckSender(RequisitesDocumentSender requisitesSender, RequisitesDocumentFromQNTSOFTController requisitesDocumentFromQNTSOFTSenderController, RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTSender)
        {
            string commentForSender = "";

            if (requisitesSender.INN != null)
            {
                //формирование адреса компании
                string SenderAddress;

                if (requisitesSender.AddrCompany == null)
                {
                    SenderAddress = requisitesSender.GetAddress(requisitesSender);
                }
                else
                {
                    SenderAddress = requisitesSender.AddrCompany;
                }

                var resInn = requisitesDocumentFromQNTSOFTSenderController.GetINNFromDocumentSender(Constants.tokenQNTSOFT, requisitesSender.INN);

                if (resInn.Equals(requisitesSender.INN))
                {
                    //установка свойств класса из QNTSOFT
                    requisitesDocumentFromQNTSOFTSender = requisitesDocumentFromQNTSOFTSenderController.GetRequisitesFromQNTSOFT(Constants.tokenQNTSOFT, resInn);

                    //метод для сравнения данных из документа с данными из АПИ
                     commentForSender = EqualsRequisitesOrganizationSender(requisitesSender, requisitesDocumentFromQNTSOFTSender, SenderAddress);

                }
                else
                    if (resInn == null)
                {
                     commentForSender = $"ИНН {requisitesSender.NameOrg} указан неверно";
                }
            }

            return commentForSender;
        }

        /// <summary>
        /// Метод для проверки реквизитов получателя
        /// </summary>
        /// <param name="requisitesRecipient"></param>
        /// <param name="requisitesDocumentFromQNTSOFTRecipientController"></param>
        /// <param name="requisitesDocumentFromQNTSOFTRecipient"></param>
        /// <returns></returns>
        public string CheckRecipient(RequisitesDocumentRecipient requisitesRecipient, RequisitesDocumentFromQNTSOFTController requisitesDocumentFromQNTSOFTRecipientController, RequisitesDocumentFromQNTSOFT requisitesDocumentFromQNTSOFTRecipient)
        {
            string commentForRecipient = "";

            if (requisitesRecipient.INN != null)
            {
                string RecipientAddress;

                if (requisitesRecipient.AddrCompany == null)
                {
                    RecipientAddress = requisitesRecipient.GetAddress(requisitesRecipient);
                }
                else
                {
                    RecipientAddress = requisitesRecipient.AddrCompany;
                }
                var resInn = requisitesDocumentFromQNTSOFTRecipientController.GetINNFromDocumentSender(Constants.tokenQNTSOFT, requisitesRecipient.INN);

                if (resInn.Equals(requisitesRecipient.INN))
                {
                    requisitesDocumentFromQNTSOFTRecipient = requisitesDocumentFromQNTSOFTRecipientController.GetRequisitesFromQNTSOFT(Constants.tokenQNTSOFT, resInn);

                     commentForRecipient = EqualsRequisitesOrganizationRecipient(requisitesRecipient, requisitesDocumentFromQNTSOFTRecipient, RecipientAddress);
                }
                else
                    if (resInn == null)
                {
                     commentForRecipient = $"ИНН {requisitesRecipient.NameOrg} указан неверно";
                }
            }

            return commentForRecipient;
        }

    }
}

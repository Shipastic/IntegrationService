using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class Facade
    {
        EDMClass eDMClass = new EDMClass();

        OracleConnectionState connectionState;
        public void SendMessagePath(Document document, ParcedDocument parcedDocument,
                                            string messageCom, string messageCom2, int maxLabel,
                                            Message mesContant, string usrId, List<Diadoc.Api.Proto.Departments.Department> departments, ResolutionType resolutionType, Contractor contractor)
        {
            WorkWithDoc  workWithDoc = new WorkWithDoc();

            MessagePathToPostIris toPost = new MessagePathToPostIris();

            DocumentsMoveOperationIris documentsMove = new DocumentsMoveOperationIris();

            RessolutionAttachmentClass attachResol = new RessolutionAttachmentClass();

            ResolutionRequestAttachmentClass requestAttachmentClass = new ResolutionRequestAttachmentClass();

            ResolutionRequestCancellationAttachmentIris cancellationAttachmentIris = new ResolutionRequestCancellationAttachmentIris();

            //Проверка новых документов
            if (parcedDocument.isArrove == 0 && parcedDocument.status == 1 && parcedDocument.cntTag != "")
            {

                //Первичная  проверка и согласование документ
                var resAttach = attachResol.SetResolutionAttach(resolutionType, messageCom, document.EntityId);

                maxLabel++;

                resAttach.AddLabel(maxLabel.ToString());

                //Создаем объект класса MessagePatchToPost
                var messageToPost = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                //Добавляем к объекту результат согласования
                messageToPost.AddResolution(resAttach);

                //Формируем дополнение к сообщению и отправляем на сервер
                var messPostConfirm = eDMClass.PostMessagePatch(messageToPost, parcedDocument.Ids);

                //Отправка на согласование дальше
                var resReqAttach = requestAttachmentClass.SetRequestAttach(ResolutionRequestType.ApprovementRequest, document.EntityId, usrId, messageCom2);

                maxLabel++;

                //parcedDocument.statusParce += messageCom2;

                resReqAttach.AddLabel(maxLabel.ToString());

                //Создаем объект класса MessagePatchToPost
                var messageToPostReq = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                messageToPostReq.AddResolutionRequestAttachment(resReqAttach);

                var mesPosToDept = eDMClass.PostMessagePatch(messageToPostReq, parcedDocument.Ids);
            }

            else
            //Добавляем автосогласование на документ по соответствующему статусу РУ
            if (parcedDocument.isArrove == 1 && parcedDocument.status == 6 && parcedDocument.cntTag != "")
            {

                string canselResolut = $"\n\n{Constants.CanselRequest} на сотрудника {parcedDocument.email}";

                parcedDocument.statusParce = canselResolut;

                var InitResreqId = mesContant.Entities
                                                 .Where(e => e.AttachmentType.Equals(AttachmentType.ResolutionRequest))
                                                 .Select(e => e.EntityId)
                                                 .FirstOrDefault();

                //Отменяем согласование
                var resReqCancAttach = cancellationAttachmentIris.SetResCancAttach(InitResreqId, canselResolut);

                var mesToPost = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                mesToPost.AddResolutionRequestCancellationAttachment(resReqCancAttach);

                var messCancellRequest = eDMClass.PostMessagePatch(mesToPost, parcedDocument.Ids);

                OracleConnectionState.SetRuApproveStatus2(contractor, mesContant.MessageId, document.EntityId);

                parcedDocument.statusParce += "\n\nАвтосогласование на Системную У.З.\n";
            
            }

            if (parcedDocument.isArrove == 2 && parcedDocument.status == 6 && parcedDocument.cntTag != "")
            {
            
                var resReqAttach = requestAttachmentClass.SetRequestAttach(ResolutionRequestType.ApprovementRequest, document.EntityId, usrId, messageCom2);

                maxLabel++;

                resReqAttach.AddLabel(maxLabel.ToString());

                //Создаем объект класса MessagePatchToPost
                var messageToPostReq = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                messageToPostReq.AddResolutionRequestAttachment(resReqAttach);

                var mesPosToDept = eDMClass.PostMessagePatch(messageToPostReq, parcedDocument.Ids);

                workWithDoc.ResolutionStatus = messageCom2;
        }
                else
                {
                var resAttach = attachResol.SetResolutionAttach(resolutionType, messageCom, document.EntityId);
                maxLabel++;

                resAttach.AddLabel(maxLabel.ToString());

                var messageToPost = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                messageToPost.AddResolution(resAttach);

                var messPostConfirm = eDMClass.PostMessagePatch(messageToPost, parcedDocument.Ids);

                var resReqAttach = requestAttachmentClass.SetRequestAttach(ResolutionRequestType.ApprovementRequest, document.EntityId, usrId, messageCom2);

                maxLabel++;

                resReqAttach.AddLabel(maxLabel.ToString());

                var messageToPostReq = toPost.SetMessPathToPost(mesContant.MessageId, Constants.DefaultFromBoxId);

                messageToPostReq.AddResolutionRequestAttachment(resReqAttach);

                var mesPosToDept = eDMClass.PostMessagePatch(messageToPostReq, parcedDocument.Ids);

            }
            //Передача документа в подразделение
            string depId = departments.Where(d => d.Abbreviation == parcedDocument.cntTag)
                                      .Select(d => d.Id)
                                      .FirstOrDefault();

            var docMove = documentsMove.SetMoveOperation(Constants.DefaultFromBoxId, depId);

            docMove.AddDocumentId(
                new DocumentId
                {
                    MessageId = mesContant.MessageId,
                    EntityId = document.EntityId
                });

            eDMClass.MoveDocuments(docMove, parcedDocument.Ids);

            maxLabel++;
            var nameDepart = departments.Where(d => d.Abbreviation == parcedDocument.cntTag)
                                      .Select(d => d.Name)
                                      .FirstOrDefault();
            parcedDocument.statusParce += $"\n\nДокумент {document.Title} перемещен  в {nameDepart}";
        }
      
    }
}

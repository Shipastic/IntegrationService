using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
    public class ContentDocumentController
    {
        /// <summary>
        /// Метод для получения списка информации о содержимм сообщении из документа для  типа вложений УниверсальныйПередаточныйДокумент
        /// </summary>
        /// <param name="messageED">цепочка сообщений </param>
        /// <returns></returns>
        public List<Content> GetContentDocuments(Message message, int maxlab)
        {
            var contents = new List<Content>();

            contents = message.Entities
                              .Where(e => e.AttachmentType == AttachmentType.UniversalTransferDocument)
                              .Select(e => e.Content)
                              .ToList();

            maxlab = message.Entities.Count;

            return contents;
        }

        /// <summary>
        /// Метод для получения списка информации о содержимм сообщении из документа для  типа вложений СчетФактура
        /// </summary>
        /// <param name="message"></param>
        /// <param name="maxlab"></param>
        /// <returns></returns>
        public List<Content> GetContentDocumentsSF(Message message, int maxlab)
        {
            var contents = new List<Content>();

            contents = message.Entities
                              .Where(e => e.AttachmentType == AttachmentType.Invoice)
                              .Select(e => e.Content)
                              .ToList();

            maxlab = message.Entities.Count;

            return contents;
        }
    }
}

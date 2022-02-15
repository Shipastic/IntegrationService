using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class ResolutionRequestAttachmentClass : ResolutionRequestAttachment
    {
        ResolutionRequestType resolutionRequestType;

        string initDocId;

        string userId;

        string comment;

        public ResolutionRequestType requestType
        {
            get
            {
                return Type;
            }
            set
            {
                Type = value;
            }
        }

        public string InitDocID
        {
            get
            {
                return InitialDocumentId;
            }
            set
            {
                InitialDocumentId = value;
            }
        }
        public string UserID
        {
            get
            {
                return TargetUserId;
            }
            set
            {
                TargetUserId = value;
            }
        }
        public string CommentNew
        {
            get
            {
                return Comment;
            }
            set
            {
                Comment = value;
            }
        }

        public ResolutionRequestAttachment SetRequestAttach(ResolutionRequestType requestType, string initialDocId, string userid, string coment)
        {
            ResolutionRequestAttachment requestAttachment = new ResolutionRequestAttachment
            {
                Comment = coment,
                Type = requestType,
                InitialDocumentId = initialDocId,
                TargetUserId = userid
            };
            return requestAttachment;
        }
    }
}

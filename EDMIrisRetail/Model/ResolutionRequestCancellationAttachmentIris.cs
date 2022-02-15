using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class ResolutionRequestCancellationAttachmentIris : ResolutionRequestCancellationAttachment
    {
        public string InitResolutionRequest
        {
            get
            {
                return InitialResolutionRequestId;
            }
            set
            {
                InitialResolutionRequestId = value;
            }
        }

        public string Com
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

        public ResolutionRequestCancellationAttachment SetResCancAttach(string InitResolRId, string _Comment)
        {
            ResolutionRequestCancellationAttachment cancellationAttachment = new ResolutionRequestCancellationAttachment
            {
                InitialResolutionRequestId = InitResolRId,
                Comment = _Comment
            };
            return cancellationAttachment;
        }
    }
}

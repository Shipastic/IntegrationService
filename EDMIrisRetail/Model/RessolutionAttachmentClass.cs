using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class RessolutionAttachmentClass : ResolutionAttachment
    {
        public ResolutionAttachment SetResolutionAttach(ResolutionType resolution, string _comment, string _initDocId)
        {
            ResolutionAttachment attachment = new ResolutionAttachment
            {
                ResolutionType = resolution,
                Comment = _comment,
                InitialDocumentId = _initDocId
            };
            return attachment;
        }

    }
}

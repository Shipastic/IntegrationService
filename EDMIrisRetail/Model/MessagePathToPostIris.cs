using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class MessagePathToPostIris : MessagePatchToPost
    {

        public string BoxidfromDep
        {
            get
            {
                return BoxId;
            }
        }

        public string MessId
        {
            get
            {
                return MessageId;
            }
            set
            {
                MessageId = value;
            }
        }

        public MessagePathToPostIris(string messageId, string boxidfromDep)
        {
            BoxId = boxidfromDep;

            MessageId = messageId;
        }

        public MessagePathToPostIris()
        {

        }

        public MessagePatchToPost SetMessPathToPost(string messageId, string boxidfromDep)
        {
            MessagePatchToPost patchToPost = new MessagePatchToPost
            {
                BoxId = boxidfromDep,

                MessageId = messageId
            };

            return patchToPost;
        }
    }
}

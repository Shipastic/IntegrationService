using Diadoc.Api.Proto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class MessageToPostIris : MessageToPost
    {
        public MessageToPost SetMessageToPost(string _fromBoxIs, string _toBoxId)
        {
            MessageToPost messageToPost = new MessageToPost
            {
                FromBoxId = _fromBoxIs,

                ToBoxId = _toBoxId
            };
            return messageToPost;
        }
    }
}

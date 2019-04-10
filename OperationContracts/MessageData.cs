using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [DataContract]
    public class MessageData
    {
        
        [DataMember]
        public CurrentUser NickName { set; get; }
        [DataMember]
        public string MessageTime { set; get; }
        [DataMember]
        public string Message { set; get; }
    }
}

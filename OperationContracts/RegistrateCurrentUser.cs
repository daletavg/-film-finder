using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [DataContract]
    public class RegistrateCurrentUser:CurrentUser
    {
        [DataMember]
        public string Password { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    
    [DataContract]
    public class CurrentUser
    {
        [DataMember]
        //string login, int age, string password, int gender, byte[] usrImage
        public string Login { set; get; }
        [DataMember]
        public string DateBirthday { set; get; }
        [DataMember]
        public int Gender { set; get; }
        [DataMember]
        public byte[] UserImage { set; get; }
    }
}

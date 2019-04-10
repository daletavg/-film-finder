using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISettings
    {
        [OperationContract]
        void UploadUserImage(byte[] image);

        [OperationContract]
        void ChangeUserProfile(CurrentUser user);
    }
}

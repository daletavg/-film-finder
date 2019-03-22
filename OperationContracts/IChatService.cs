using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(MessageData msg);

        [OperationContract(IsOneWay = true)]
        void CloseConnection();
        [OperationContract(IsOneWay = true)]
        void AddNewUserChatService();
    }

    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void SetMessage(MessageData msg);
      
    }
}

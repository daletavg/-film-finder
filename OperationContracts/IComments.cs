using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IComments
    {
        [OperationContract]
        void AddComment(string filmName,string comment);

        [OperationContract]
        MessageData GetComments(int index, string filmName);

        [OperationContract]
        int GetCountComments(string filmName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OperationContracts
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ILoginRegisterUser
    {

        [OperationContract]
        int CheckUserOnDB(string login, string password);

        [OperationContract]
        CurrentUser GetCurrentUser();
        [OperationContract]
        void AddNewUserOnDB(RegistrateCurrentUser registrate);
    }
}

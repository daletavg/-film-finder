using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;

namespace Server_Films
{
    public class LoginRegisterUser:ILoginRegisterUser
    {
        public bool CheckUserOnDB(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void AddNewUserOnDB(string login, int age, string password, int gender, byte[] usrImage)
        {
            throw new NotImplementedException();
        }
    }
}

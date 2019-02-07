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
            using (var db = new FilmFinderDB())
            {
                var checkUser = db.Users.All(i => i.Name == login && i.Password == password);
                return checkUser;
            }
            
        }

        public void AddNewUserOnDB(string login, int age, string password, int gender, byte[] usrImage)
        {
            using (var db = new FilmFinderDB())
            {
                bool tmpGender = true;
                switch (gender)
                {
                    case 0:
                        tmpGender = false;
                        break;
                    case 1:
                        tmpGender = true;
                        break;
                }

                db.Users.Add(new User() {Name = login, Password = password, Gender = tmpGender});
                db.SaveChanges();
            }
        }
    }
}

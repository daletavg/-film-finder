using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;

namespace Server_Films
{
    public class CheckUserOnApp
    {
        public UResult CheckUser(string login, string password)
        {
            using (var db = new FilmFinderDB())
            {
                GetHeshMd5 getHesh = new GetHeshMd5();
                var checkUser = db.Users.First(i => i.Name == login );
                if (checkUser==null)
                {
                    return UResult.UserFailed;
                }
                else
                {
                    if (checkUser.Password == getHesh.GetHesh(password))
                    {
                        return UResult.Access;
                    }
                    else
                    {
                        return UResult.PasswordFailed;
                    }
                }

                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;
using Server_Films.Film_finder;

namespace Server_Films
{
    public class CheckUserOnApp
    {
        public CurrentUser CurrentUser { private set; get; } = null;

        public UResult CheckUser(string login, string password)
        {
            using (var db = new FilmFinderDB())
            {
                
                GetHeshMd5 getHesh = new GetHeshMd5();
                User checkUser =  new User();
                try
                {
                    checkUser = db.Users.First(i => i.Name == login);
                }
                catch (Exception e)
                {
                    db.Dispose();
                    return UResult.UserFailed;
                }

                if (checkUser==null)
                {
                    return UResult.UserFailed;
                }
                else
                {
                    if (checkUser.Password == getHesh.GetHesh(password))
                    {
                        SetCurrentUser(checkUser);
                        return UResult.Access;
                       
                    }
                    else
                    {
                        return UResult.PasswordFailed;
                    }
                }

                
            }
        }

        void SetCurrentUser(User checkUser)
        {
            var currentUser = new CurrentUser();
            currentUser.Login = checkUser.Name;
            currentUser.DateBirthday = checkUser.DateBirthday;
            currentUser.Gender = checkUser.Gender ? 1 : 0;
            currentUser.UserImage = checkUser.UserImage;
            CurrentUser = currentUser;
        }
    }
}

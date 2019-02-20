using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;

namespace Server_Films
{
    public class FilmsFinderServer:ILoginRegisterUser, IAddLoadFilm
    {
        public bool CheckUserOnDB(string login, string password)
        {
            using (var db = new FilmFinderDB())
            {
                GetHeshMd5 getHesh = new GetHeshMd5();
                var checkUser = db.Users.All(i => i.Name == login && i.Password == getHesh.GetHesh(password));
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
                GetHeshMd5 getHesh = new GetHeshMd5();
                
                db.Users.Add(new User() {Name = login, Password = getHesh.GetHesh(password), Gender = tmpGender});
                db.SaveChanges();
            }
        }

        public void AddNewFilm(FilmContent content)
        {
            throw new NotImplementedException();
        }

        public FilmContent GetAllFilms()
        {
            throw new NotImplementedException();
        }

        public AllSpecificAddingFilm GetSpecific()
        {
            AllSpecificAddingFilm tmp = new AllSpecificAddingFilm();
            using (var db = new FilmFinderDB())
            {
                var tmpActors = db.Actors.ToArray();
                tmp.Actors = new string[tmpActors.Length];
                for (int i = 0; i < tmpActors.Length; i++)
                {
                    tmp.Actors[i] = tmpActors[i].Name;          
                }

                var tmpGener = db.Genres.ToArray();
                tmp.Geners = new string[tmpGener.Length];
                for (int i = 0; i < tmpGener.Length; i++)
                {
                    tmp.Geners[i] = tmpGener[i].Genr;
                }

                var tmpProdusser = db.Producers.ToArray();
                tmp.Produsers= new string[tmpProdusser.Length];
                for (int i = 0; i < tmpProdusser.Length; i++)
                {
                    tmp.Produsers[i] = tmpProdusser[i].Name;
                }
            }

            return tmp;

        }
    }
}

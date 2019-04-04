﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;
using System.Drawing;
using System.Drawing.Imaging;
using Server_Films.Film_finder;
using System.ServiceModel;

namespace Server_Films
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,ConcurrencyMode = ConcurrencyMode.Single)]
    public class FilmsFinderServer: IFilmFinderServer,IAddLoadFilm,ILoginRegisterUser,ISetRaiting, IChatService
    {
        
        private CurrentUser _currentUser = new CurrentUser();


        public FilmsFinderServer()
        {
            //AddNewUserChatService();
        }


        public int CheckUserOnDB(string login, string password)
        {
            var user = new CheckUserOnApp();
            var userResult = user.CheckUser(login,password);
            if (userResult==UResult.Access)
            {
                _currentUser = user.CurrentUser;
            }

            return (int)userResult;

        }

        public void AddNewUserOnDB(RegistrateCurrentUser registrate)
        {
            using (var db = new FilmFinderDb())
            {
                bool tmpGender = true;
                switch (registrate.Gender)
                {
                    case 0:
                        tmpGender = false;
                        break;
                    case 1:
                        tmpGender = true;
                        break;
                }
                GetHeshMd5 getHesh = new GetHeshMd5();
                
                db.Users.Add(new User() {Name = registrate.Login, Password = getHesh.GetHesh(registrate.Password), Gender = tmpGender});
                db.SaveChanges();
            }
        }

        public void AddNewFilm(FilmContent content)
        {
            CreateNewFilm createNewFilm = new CreateNewFilm(content,_currentUser);
            createNewFilm.Create();
        }

        public FilmContent GetFilm(int index)
        {
            FilmContent newFilm = new FilmContent();
            using (var db = new FilmFinderDb())
            {
                var film = db.Films.ToArray()[index];
                newFilm.Name = film.Name;
                newFilm.Description = film.Description;
                
                
                newFilm.ReleaseDate = film.ReleaseDate;

                var actors = db.ActorToFilms.ToArray().Where(i => i.Film == film).ToArray();
                newFilm.Actors = new string[actors.Length];
                
                for (int i = 0; i < actors.Length; i++)
                {
                    newFilm.Actors[i] = actors[i].Actor.Name;
                }

                //newFilm.Image = File.ReadAllBytes(film.Image);


                /////////////////
                Bitmap btm = new Bitmap(film.Image);
                using (MemoryStream ms = new MemoryStream())
                {
                    btm.Save(ms, ImageFormat.Jpeg);
                    btm.Dispose();
                    newFilm.Image = ms.GetBuffer();
                }

                ///////////////

                var produssers = db.ProdusserToFilms.ToArray().Where(i => i.Film == film).ToArray();
                newFilm.Produsers = new string[produssers.Length];
                for (int i = 0; i < produssers.Length; i++)
                {
                    newFilm.Produsers[i] = produssers[i].Producer.Name;
                }

                var genrs = db.GenreToFilms.ToArray().Where(i => i.Film == film).ToArray();
                newFilm.Geners = new string[genrs.Length];
                for (int i = 0; i < genrs.Length; i++)
                {
                    newFilm.Geners[i] = genrs[i].Genre.Genr;
                }
            }

            return newFilm;
        }

        public int GetFilmsCount()
        {
            int count;
            using (var db = new FilmFinderDb())
            {
                count=db.Films.Count();
            }

            return count;
        }

        public AllSpecificAddingFilm GetSpecific()
        {
            AllSpecificAddingFilm tmp = new AllSpecificAddingFilm();
            using (var db = new FilmFinderDb())
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

        public CurrentUser GetCurrentUser()
        {
            return _currentUser;
        }




        static List<IChatCallback> Chats = new List<IChatCallback>();
        static List<CurrentUser> Users = new List<CurrentUser>();
        private IChatCallback Current
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IChatCallback>();
            }
        }

        public void AddNewUserChatService()
        {
            Chats.Add(Current);
        }

        public void SendMessage(MessageData msg)
        {
            //Current.SetMessage(msg);
            foreach (var i in Chats)
            {
                i.SetMessage(msg);
            }
        }

        public void CloseConnection()
        {
            Chats.Remove(Current);
        }


        public void SetRaiting(int raiting, string nameOfFilm)
        {
            using (var db = new FilmFinderDb())
            {
                var tmpUser = db.Users.First(j => j.Name == _currentUser.Login);
                if (db.Marks.Any(i =>i.Film.Name==nameOfFilm && i.User.Name == tmpUser.Name))
                {
                    var tmpMark = db.Marks.First(i => i.Film.Name == nameOfFilm && i.User.Name == tmpUser.Name);
                    tmpMark.Marks = raiting;
                    db.SaveChanges();
                    return;
                }

                Mark mark = new Mark();
                mark.Film = db.Films.ToList().First(i => i.Name == nameOfFilm);
                mark.User = db.Users.ToList().First(i => i.Name == _currentUser.Login);
                mark.Marks = raiting;
                db.Marks.Add(mark);
                db.SaveChanges();

            }
        }

        public float GetRaitingOfFilm(string nameOfFilm)
        {
            float middleRaiting = 0;
            using (var db = new FilmFinderDb())
            {
                int counter = 0;
                foreach (var mark in db.Marks.ToList().Where(i=>i.Film.Name==nameOfFilm))
                {
                    middleRaiting += mark.Marks;
                    counter++;
                }

                if (counter != 0)
                    middleRaiting = middleRaiting / counter;
            }

            return (float)Math.Round(middleRaiting, 2);

        }
    }
}

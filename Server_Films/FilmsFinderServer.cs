using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;

namespace Server_Films
{
    public class FilmsFinderServer:ILoginRegisterUser, IAddLoadFilm
    {
        public int CheckUserOnDB(string login, string password)
        {
            return (int) new CheckUserOnApp().CheckUser(login, password);

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
            using (var db = new FilmFinderDB())
            {
                var newFilm = new Film()
                {
                    Name = content.Name,
                    Description = content.Description,
                    ReleaseDate = content.ReleaseDate,
                    Image = @"/Films_images/" + content.Name.Replace(" ", "_")+"/"+content.ImageName.Replace(" ","_")
                };
                
                string path = @"/Films_images/" + content.Name.Replace(" ", "_");
                Directory.CreateDirectory(path);

                using (BinaryWriter fstream = new BinaryWriter(File.Open(newFilm.Image, FileMode.OpenOrCreate)))
                {
                        // преобразуем строку в байты
                        byte[] array = Encoding.Default.GetBytes(content.Image);
                        // запись массива байтов в файл
                        fstream.Write(array);
                        Console.WriteLine("Текст записан в файл");
                }
                

                db.Films.Add(newFilm);

                for (int i = 0; i < content.Actors.Length; i++)
                {
                    var actorToFilm = new ActorToFilm();
                    actorToFilm.Film = newFilm;
                    Actor[] tmp = db.Actors.ToList().Where(j => j.Name == content.Actors[i]).ToArray();
                    
                    
                    
                    if (tmp.Length == 0)
                    {
                        Actor actor = new Actor() {Name = content.Actors[i]};
                        db.Actors.Add(actor);
                        actorToFilm.Actor = actor;
                        db.ActorToFilms.Add(actorToFilm);
                        continue;
                    }
                    actorToFilm.Actor = tmp[0];
                    db.ActorToFilms.Add(actorToFilm);



                }
                for (int i = 0; i < content.Geners.Length; i++)
                {
                    var genreToFilm = new GenreToFilm();
                    genreToFilm.Film = newFilm;
                    var tmp = db.Genres.ToList().Where(j => j.Genr == content.Geners[i]).ToList();
                    if (tmp.Count == 0)
                    {
                        Genre genre = new Genre() { Genr = content.Geners[i] };
                        db.Genres.Add(genre);
                        genreToFilm.Genre = genre;
                        db.GenreToFilms.Add(genreToFilm);
                        continue;
                    }

                    genreToFilm.Genre = tmp[0];
                    db.GenreToFilms.Add(genreToFilm);

                }
                for (int i = 0; i < content.Produsers.Length; i++)
                {
                    var produsserToFilm = new ProdusserToFilm();
                    produsserToFilm.Film = newFilm;
                    var tmp = db.Producers.ToList().Where(j => j.Name == content.Produsers[i]).ToList();
                    if (tmp.Count == 0)
                    {
                        Producer producer = new Producer() { Name = content.Produsers[i] };
                        db.Producers.Add(producer);
                        produsserToFilm.Producer = producer;
                        db.ProdusserToFilms.Add(produsserToFilm);
                        continue;
                    }

                    produsserToFilm.Producer = tmp[0];
                    db.ProdusserToFilms.Add(produsserToFilm);

                }

                db.SaveChanges();


            }
        }

        public FilmContent GetFilm(int index)
        {
            FilmContent newFilm = new FilmContent();
            using (var db = new FilmFinderDB())
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

                newFilm.Image = Encoding.Default.GetString(File.ReadAllBytes(film.Image));

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
            using (var db = new FilmFinderDB())
            {
                count=db.Films.Count();
            }

            return count;
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

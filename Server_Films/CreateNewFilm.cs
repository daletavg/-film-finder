using OperationContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server_Films.Film_finder;

namespace Server_Films
{
    public class CreateNewFilm
    {
        public FilmContent FilmContent { set; get; }
        public Film _film;
        private CurrentUser _currentUser;
        public CreateNewFilm (FilmContent filmContent,CurrentUser currentUser)
        {
            _currentUser = currentUser;
            FilmContent = filmContent;
            _film = CreateFilmObject();
            
        }


        public Film CreateFilmObject()
        {
            Film newFilm;
            using (var db = new FilmFinderDb())
            {
               

                newFilm = new Film()
                {
                    Name = FilmContent.Name,
                    Description = FilmContent.Description,
                    ReleaseDate = FilmContent.ReleaseDate,
                    User = db.Users.ToList().First(i => i.Name == _currentUser.Login),
                    Image = @"./Films_images/" + FilmContent.Name.Replace(" ", "_") + "/" +
                                FilmContent.ImageName.Replace(" ", "_")
                };
                newFilm.TimeFilm = FilmContent.FilmTime;
            }

            return newFilm;
        }


        public void CreateFolderToImageFilm()
        {
            string path = @"./Films_images/" + FilmContent.Name.Replace(" ", "_");
            Directory.CreateDirectory(path);

            using (BinaryWriter fstream = new BinaryWriter(File.Open(@"./Films_images/" + FilmContent.Name.Replace(" ", "_") + "/" + FilmContent.ImageName.Replace(" ", "_"), FileMode.OpenOrCreate)))
            {
                // преобразуем строку в байты
                byte[] array = FilmContent.Image;
                // запись массива байтов в файл
                fstream.Write(array);
                Console.WriteLine("Текст записан в файл");
            }

        }
        public void Create()
        {
            CreateFolderToImageFilm();
            using (var db = new FilmFinderDb())
            {
                db.Films.Add(_film);
                CreateActorsObject(db);
                CreateGenersObject(db);
                CreateProdussersObject(db);
                db.SaveChanges();
            }
        }


        public void CreateActorsObject(FilmFinderDb db)
        {
            
            for (int i = 0; i < FilmContent.Actors.Length; i++)
            {
                var actorToFilm = new ActorToFilm();
                actorToFilm.Film = _film;
                Actor[] tmp = db.Actors.ToList().Where(j => j.Name == FilmContent.Actors[i]).ToArray();



                if (tmp.Length == 0)
                {
                    Actor actor = new Actor() { Name = FilmContent.Actors[i] };
                    db.Actors.Add(actor);
                    actorToFilm.Actor = actor;
                    db.ActorToFilms.Add(actorToFilm);
                    continue;
                }
                actorToFilm.Actor = tmp[0];
                db.ActorToFilms.Add(actorToFilm);
            }
        }
        public void CreateGenersObject(FilmFinderDb db)
        {
            for (int i = 0; i < FilmContent.Geners.Length; i++)
            {
                var genreToFilm = new GenreToFilm();
                genreToFilm.Film = _film ;
                var tmp = db.Genres.ToList().Where(j => j.Genr == FilmContent.Geners[i]).ToList();
                if (tmp.Count == 0)
                {
                    Genre genre = new Genre() { Genr = FilmContent.Geners[i] };
                    db.Genres.Add(genre);
                    genreToFilm.Genre = genre;
                    db.GenreToFilms.Add(genreToFilm);
                    continue;
                }

                genreToFilm.Genre = tmp[0];
                db.GenreToFilms.Add(genreToFilm);

            }
        }

        public void CreateProdussersObject(FilmFinderDb db)
        {
            for (int i = 0; i < FilmContent.Produsers.Length; i++)
            {
                var produsserToFilm = new ProdusserToFilm();
                produsserToFilm.Film = _film;
                var tmp = db.Producers.ToList().Where(j => j.Name == FilmContent.Produsers[i]).ToList();
                if (tmp.Count == 0)
                {
                    Producer producer = new Producer() { Name = FilmContent.Produsers[i] };
                    db.Producers.Add(producer);
                    produsserToFilm.Producer = producer;
                    db.ProdusserToFilms.Add(produsserToFilm);
                    continue;
                }

                produsserToFilm.Producer = tmp[0];
                db.ProdusserToFilms.Add(produsserToFilm);

            }
        }


    }
}

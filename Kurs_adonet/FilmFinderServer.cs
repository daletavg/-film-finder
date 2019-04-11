
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperationContracts;

namespace Kurs_adonet
{
    class FilmFinderServer : OperationContracts.IFilmFinderServer
    {

        ServiceFF.IFilmFinderServer _server;

        public FilmFinderServer()
        {
            _server = new ServiceFF.FilmFinderServerClient();
        }

        public int AddNewFilm(FilmContent content)
        {
            return _server.AddNewFilm(content);
        }

        public int AddNewUserOnDB(RegistrateCurrentUser registrate)
        {
            return _server.AddNewUserOnDB(registrate);
        }

        public int CheckUserOnDB(string login, string password)
        {
            return _server.CheckUserOnDB(login, password);
        }

        

        public CurrentUser GetCurrentUser()
        {
            return _server.GetCurrentUser();
        }

        public FilmContent GetFilm(int index)
        {
            return _server.GetFilm(index);
        }

        public FilmContent GetFavoritFilms(int index)
        {
            return _server.GetFavoritFilms(index);
        }

        public int GetFilmsCount()
        {
            return _server.GetFilmsCount();
        }

        public int GetFavoritFilmsCount()
        {
            return _server.GetFavoritFilmsCount();
        }

        public AllSpecificAddingFilm GetSpecific()
        {
            return _server.GetSpecific();
        }

        public void SetFavorit(string filmName, bool isFavorit)
        {
            _server.SetFavorit(filmName,isFavorit);
        }


        public void SetRaiting(int raiting, string nameOfFilm)
        {
           _server.SetRaiting(raiting,nameOfFilm);
        }

        public float GetRaitingOfFilm(string nameOfFilm)
        {
            return _server.GetRaitingOfFilm(nameOfFilm);
        }

        public int GetCurrentRaiting(string nameOfFilm)
        {
            return _server.GetCurrentRaiting(nameOfFilm);
        }

        public void UploadUserImage(byte[] image)
        {
            _server.UploadUserImage(image);
        }

        public void ChangeUserProfile(RegistrateCurrentUser user)
        {
            _server.ChangeUserProfile(user);
        }

        public void AddComment(string filmName, string comment)
        {
            _server.AddComment(filmName,comment);
        }

        public MessageData GetComments(int index, string filmName)
        {
            return _server.GetComments(index, filmName);
        }

        public int GetCountComments(string filmName)
        {
            return _server.GetCountComments(filmName);
        }
    }
}

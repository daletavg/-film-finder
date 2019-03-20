
using OperationContracts;
using Kurs_adonet.FilmsFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_adonet
{
    class FilmFinderServer : OperationContracts.IFilmFinderServer
    {

        private Kurs_adonet.FilmsFinder.IFilmFinderServer _server;

        public FilmFinderServer()
        {
            _server = new Kurs_adonet.FilmsFinder.FilmFinderServerClient();
        }

        public void AddNewFilm(FilmContent content)
        {
            _server.AddNewFilm(content);
        }

        public void AddNewUserOnDB(RegistrateCurrentUser registrate)
        {
            _server.AddNewUserOnDB(registrate);
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

        public int GetFilmsCount()
        {
            return _server.GetFilmsCount();
        }

        public AllSpecificAddingFilm GetSpecific()
        {
            return _server.GetSpecific();
        }
    }
}

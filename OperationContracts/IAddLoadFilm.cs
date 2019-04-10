
using System.ServiceModel;


namespace OperationContracts
{

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAddLoadFilm
    {
        [OperationContract]
        int AddNewFilm(FilmContent content);
        [OperationContract]
        FilmContent GetFilm(int index);

        [OperationContract]
        AllSpecificAddingFilm GetSpecific();

        [OperationContract]
        void SetFavorit(string filmName, bool isFavorit);

        [OperationContract]
        FilmContent GetFavoritFilms(int index);

        [OperationContract]
        int GetFilmsCount();
        [OperationContract]
        int GetFavoritFilmsCount();
    }
}
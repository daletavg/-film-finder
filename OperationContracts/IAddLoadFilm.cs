
using System.ServiceModel;


namespace OperationContracts
{
    [ServiceContract]
    public interface IAddLoadFilm
    {   
        [OperationContract]
        void AddNewFilm(FilmContent content);
        [OperationContract]
        FilmContent GetAllFilms();

        [OperationContract]
        AllSpecificAddingFilm GetSpecific();

    }
}

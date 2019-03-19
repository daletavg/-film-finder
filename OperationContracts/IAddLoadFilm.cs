
using System.ServiceModel;


namespace OperationContracts
{
    
    [ServiceContract(SessionMode=SessionMode.Required)]
    public interface IAddLoadFilm
    {   
        [OperationContract]
        void AddNewFilm(FilmContent content);
        [OperationContract]
        FilmContent GetFilm(int index);

        [OperationContract]
        AllSpecificAddingFilm GetSpecific();

        [OperationContract]
        int GetFilmsCount();
    }
}

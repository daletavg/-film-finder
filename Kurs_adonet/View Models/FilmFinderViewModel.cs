
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kurs_adonet.Annotations;
using OperationContracts;

namespace Kurs_adonet
{
    public class FilmFinderViewModel : INotifyPropertyChanged
    {
        private ILoginRegisterUser loginRegister;
        private CurrentUser _currentUser;


        public FilmFinderViewModel(ILoginRegisterUser logRegUser)
        {
            loginRegister = logRegUser;
            
        }

        public void LoadUser()
        {
            _currentUser = loginRegister.GetCurrentUser();
            UserName = _currentUser.Login;
            AddImage();
        }

        public  string UserName { set; get; }
        private ImageSource _userImage;

        public ImageSource UserImage
        {
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
            get { return _userImage; }
        }

        private void AddImage()
        {
            UserImage = new ImageConverter().ByteToBitmapImage(_currentUser.UserImage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Kurs_adonet.Annotations;
using OperationContracts;

namespace Kurs_adonet
{
    public class SettingViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action LoadUser;
        private ISettings _settings;
        private CurrentUser _currentUser;

        public SettingViewModel(ISettings settings)
        {
            _settings = settings;
       
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string PathToImage { set; get; }
        public ImageSource _userImage = null;

        public ImageSource UserImage
        {
            set { _userImage = value;OnPropertyChanged(nameof(UserImage)); }
            get { return _userImage; }
        }

        public void UploadImage()
        {
            var img = File.ReadAllBytes(PathToImage);
            _settings.UploadUserImage(img);
            LoadUser();
        }

        public void LoadUserToSetting()
        {
            _currentUser = ((ILoginRegisterUser)_settings).GetCurrentUser();
            ShowImage();
        }

        private void ShowImage()
        {
            UserImage = new ImageConverter().ByteToBitmapImage(_currentUser.UserImage);
        }

    }
}

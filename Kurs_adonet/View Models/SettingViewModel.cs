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
        private DateTime _dateBirthday;
        public DateTime DateBirthday
        {
            set { _dateBirthday = value;OnPropertyChanged(nameof(DateBirthday)); }
            get { return _dateBirthday; }
        }

        public ImageSource UserImage
        {
            set { _userImage = value;OnPropertyChanged(nameof(UserImage)); }
            get { return _userImage; }
        }
        #region Gender




        int _gender;
        internal int Gender
        {
            get { return _gender; }
            set
            {
                if (_gender == value)
                {
                    return;
                }
                _gender = value;
                OnPropertyChanged(nameof(Gender));
                OnPropertyChanged(nameof(Male));
                OnPropertyChanged(nameof(Female));
            }
        }
        public bool Male
        {
            get { return Gender == 1; }
            set { Gender = value ? 1 : Gender; }
        }
        public bool Female
        {
            get { return Gender == 0; }
            set { Gender = value ? 0 : Gender; }
        }

        #endregion
        public void UploadImage()
        {
            var img = File.ReadAllBytes(PathToImage);
            _settings.UploadUserImage(img);
            LoadUser();
        }

        public string Password { set; get; } = "";
        public string SecondPassword { set; get; } = "";
        private string _errorMessage = "";

        public string ErrorMessage
        {
            set { _errorMessage = value;OnPropertyChanged(nameof(ErrorMessage)); }
            get { return _errorMessage; }
        }
        public void LoadUserToSetting()
        {
            _currentUser = ((ILoginRegisterUser)_settings).GetCurrentUser();
            if (_currentUser.DateBirthday==null|| _currentUser.DateBirthday=="")
            {
                DateBirthday = DateTime.Parse("1/1/1999");
            }
            else
            {
                DateBirthday = DateTime.Parse(_currentUser.DateBirthday);
            }

            Gender = _currentUser.Gender;
            ShowImage();
        }

        private void ShowImage()
        {
            UserImage = new ImageConverter().ByteToBitmapImage(_currentUser.UserImage);
        }

        public void ChangeUser()
        {
            RegistrateCurrentUser usr = new RegistrateCurrentUser();
            if (Password!="")
            {
                if (Password == ""  || SecondPassword == "" )
                {
                    ErrorMessage = "*Пароли не совпадают";
                    return;
                }
                else if (Password!=SecondPassword)
                {
                    ErrorMessage = "*Пароли не совпадают";
                    return;
                }
                else
                {
                    usr.Password = Password;
                }
            }

            usr.Gender = Gender;
            usr.DateBirthday = DateBirthday.ToString();
            _settings.ChangeUserProfile(usr);
        }

    }
}

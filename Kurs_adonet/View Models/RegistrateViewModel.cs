using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Kurs_adonet.Annotations;
using Kurs_adonet.FilmsFinder;
using OperationContracts;
using OperationContracts;

namespace Kurs_adonet
{
    class RegistrateViewModel:INotifyPropertyChanged
    {
        OperationContracts.ILoginRegisterUser _loginRegister;
        public RegistrateViewModel(Action openRegistrate, OperationContracts.ILoginRegisterUser loginRegister)
        {
            OpenLogin=new DelegateCommand(param=>openRegistrate(),null);
            _loginRegister = loginRegister;
        }

        public ICommand OpenLogin { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime DateBirthday { set; get; }
        public string Name { set; get; } = "";
        public string Password { set; get; } = "";
        public string NewPassword { set; get; } = "";

        

        private DelegateCommand _registrate;

        public ICommand Registrate
        {
            get
            {
                if (_registrate==null)
                {
                    _registrate = new DelegateCommand(param => RegistrateOnApp(param),param=> CanExecuteRegistrate());
                }

                return _registrate;
            }
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

        public void RegistrateOnApp(object param)
        {
            var passwordBox = param as PasswordBox;
            if (passwordBox == null)
                return;
            var password = passwordBox.Password;

            byte[] tmp = { };
            RegistrateCurrentUser user = new RegistrateCurrentUser();
            user.Login = Name;
            user.Password = password;
            user.Gender = Gender;
            user.UserImage = tmp;
            user.DateBirthday = DateBirthday.ToString();
            _loginRegister.AddNewUserOnDB(user);
            OpenLogin.Execute(null);
        }

        public bool CanExecuteRegistrate()
        {
            return true;
        }

    }
}

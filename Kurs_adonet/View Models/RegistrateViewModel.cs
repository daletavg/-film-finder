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
using Kurs_adonet.LoginAndRegistrate;

namespace Kurs_adonet
{
    class RegistrateViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { set; get; } = "";
        public string Password { set; get; } = "";
        public string NewPassword { set; get; } = "";

        ILoginRegisterUser _loginRegister = new LoginRegisterUserClient();

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
            _loginRegister.AddNewUserOnDB(Name, 0, password,Gender,tmp);
        }

        public bool CanExecuteRegistrate()
        {
            return true;
        }

    }
}

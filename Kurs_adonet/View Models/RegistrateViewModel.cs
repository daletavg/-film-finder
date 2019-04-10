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
        public DateTime DateBirthday { set; get; } = DateTime.Now;
        public string Name { set; get; } = "";
       
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

        private string _errorMessage=null;

        public string ErrorMessage
        {
            set { _errorMessage = value;OnPropertyChanged(nameof(ErrorMessage)); }
            get { return _errorMessage; }
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

        public string Password { set; get; } = "";
        public string PasswordSecond { set; get; } = "";

        public void RegistrateOnApp(object param)
        {
            RegistrateCurrentUser user = new RegistrateCurrentUser();
            if (Name =="" || Password==""||PasswordSecond=="")
            {
                ErrorMessage = "*Все поля должны быть заполнены";
                return;
            }
            else if (DateBirthday > DateTime.Now)
            {
                ErrorMessage = "*Возраст не может быть больше текущей даты";
                return;
            }
            else if (Password!=PasswordSecond)
            {
                ErrorMessage = "*Пароли не совпадают";
                return;
            }
            

            byte[] tmp = { };
            
            user.Login = Name;
            user.Password = Password;
            user.Gender = Gender;
            user.UserImage = tmp;
            
            user.DateBirthday = DateBirthday.ToString();
            UResult result = (UResult) _loginRegister.AddNewUserOnDB(user);
            if (result==UResult.Access)
            {
                OpenLogin.Execute(null);
            }
            else if(result == UResult.UserFailed)
            {
                ErrorMessage = "*Такой пользователь уже существует";
            }
            
        }

        public bool CanExecuteRegistrate()
        {
            return true;
        }

    }
}

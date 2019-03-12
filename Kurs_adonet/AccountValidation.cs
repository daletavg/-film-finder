using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_adonet
{

    class AccountValidation
    {
        public event Action<string> OnPropertyChanged;
        private Brush _loginBrush;
        private Brush _passwordBrush;
        public Brush LoginBrush
        {
            set { _loginBrush = value; OnPropertyChanged(nameof(LoginBrush)); }
            get { return _loginBrush; }
        } 
        public Brush PasswordBrush
        {
            set { _passwordBrush = value; OnPropertyChanged(nameof(PasswordBrush)); }
            get { return _passwordBrush; }
        }
        public string ErrorMessage { set; get; }

        

    }
}

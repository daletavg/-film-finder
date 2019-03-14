using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kurs_adonet
{
    public delegate void OpenRegisterWindow(); 
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl,IUsingControl
    {
        public LoginControl()
        {
            InitializeComponent();
           
        }

        public event OpenRegisterWindow Registration;

        void OpenRegister(object o, EventArgs e)
        {
            Registration();
        }

        public CurrentControl ThisControl
        {
            get { return CurrentControl.LoginControl; }
        }
    }
}

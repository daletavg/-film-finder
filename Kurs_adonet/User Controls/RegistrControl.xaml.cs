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
    /// <summary>
    /// Interaction logic for RegistrControl.xaml
    /// </summary>
    public partial class RegistrControl : UserControl
    {
        public RegistrControl()
        {
            InitializeComponent();
            
        }
        public event OpenRegisterWindow BackToLogin;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            BackToLogin();
        }
    }
}

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogControl.Registration += OpenRegistr;
            RegControl.BackToLogin += OpenLogin;
        }

        void OpenLogin()
        {
            LogControl.Visibility = Visibility.Visible;
            RegControl.Visibility = Visibility.Hidden;
        }
        void OpenRegistr()
        {
            LogControl.Visibility = Visibility.Hidden;
            RegControl.Visibility = Visibility.Visible;
        }
    }
}

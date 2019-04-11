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
using Microsoft.Win32;

namespace Kurs_adonet.User_Controls
{
    /// <summary>
    /// Interaction logic for SettingControl.xaml
    /// </summary>
    public partial class SettingControl : UserControl
    {
        public SettingControl()
        {
            InitializeComponent();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                var filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();
                ImageSource image = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                UserImage.ImageSource = image;
                UserImage.Stretch = Stretch.Uniform;
                ((SettingViewModel) this.DataContext).PathToImage = filePath;
                ((SettingViewModel)this.DataContext).UploadImage();
            }

        }

        public void LoadUser()
        {
            ((SettingViewModel) this.DataContext).LoadUserToSetting();
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            ((SettingViewModel) this.DataContext).Password = PasswordBox.Password;
            ((SettingViewModel) this.DataContext).SecondPassword = SecondPasswordBox.Password;

          ((SettingViewModel)this.DataContext).ChangeUser();
        }
    }
}

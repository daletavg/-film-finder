using System;
using System.Collections.Generic;
using System.IO;
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
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace Kurs_adonet
{
    /// <summary>
    /// Interaction logic for AddFilm.xaml
    /// </summary>
    public partial class AddFilm : UserControl
    {
        public AddFilm()
        {
            InitializeComponent();
           
            for (int i = 0; i <= 24; i++)
            {
                Hours.Items.Add(i);
            }

            for (int i = 0; i <= 59; i++)
            {
                Minutes.Items.Add(i);
            }


            for (int i = 0; i <=59; i++)
            {
                Seconds.Items.Add(i);
            }

        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog()==true)
                {
                    //Get the path of specified file
                    var filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    ImageSource image = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                    FilmPoster.Source = image;
                    FilmPoster.Stretch = Stretch.Uniform;
                    PathToImage.Text = filePath;
                }
            
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            if (((AddFilmViewModel) DataContext).AddNewFilm())
            {
                ((AddFilmViewModel)DataContext).CloseWindow();
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            ((AddFilmViewModel) DataContext).CloseWindow();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}

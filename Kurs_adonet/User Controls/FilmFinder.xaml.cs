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
    /// Interaction logic for FilmFinder.xaml
    /// </summary>
    public partial class FilmFinder : UserControl,IUsingControl
    {
        bool isFirstStart = true;
        List<string> menuList = new List<string> { "Все фильмы", "Мои фильмы", "Настройки", "Чат", "О проекте" };

        public FilmFinder()
        {
            InitializeComponent();
            DemoItemsListBox.SelectedIndex = 0;
            CheckVisibilityControls();
        }
        private void DemoItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFirstStart)
            {
                PageName.Content = menuList[DemoItemsListBox.SelectedIndex];
                BackButton.IsChecked = false;
                CheckVisibilityControls();

            }
            isFirstStart = false;

        }

        void CheckVisibilityControls()
        {
            switch (DemoItemsListBox.SelectedIndex)
            {
                case 0:
                    this.AllFilms.Visibility = Visibility.Visible;
                    this.ChatControl.Visibility = Visibility.Hidden;
                    this.SettingControl.Visibility = Visibility.Hidden;
                    this.FavoritFilmsControl.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    this.FavoritFilmsControl.Visibility = Visibility.Visible;
                    this.AllFilms.Visibility = Visibility.Hidden;
                    this.ChatControl.Visibility = Visibility.Hidden;
                    this.SettingControl.Visibility = Visibility.Hidden;
                    
                    break;
                case 2:
                    this.SettingControl.Visibility = Visibility.Visible;
                    this.SettingControl.LoadUser();
                    this.AllFilms.Visibility = Visibility.Hidden;
                    this.ChatControl.Visibility = Visibility.Hidden;
                    this.FavoritFilmsControl.Visibility = Visibility.Hidden;
                    break;;
                case 3:
                    this.ChatControl.Visibility = Visibility.Visible;
                    this.AllFilms.Visibility = Visibility.Hidden;
                    this.SettingControl.Visibility = Visibility.Hidden;
                    this.FavoritFilmsControl.Visibility = Visibility.Hidden;
                    break;
                default:
                    
                    break;
            }
        }

        public CurrentControl ThisControl
        {
            get { return CurrentControl.FilmFinderControl; }
        }

        private void AllFilms_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                ((FilmsViewModel)AllFilms.DataContext).ShowAllFilms();
            }
        }

        private void FavoritFilmsControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue == true)
            {
                ((FavoritFilmsViewModel)FavoritFilmsControl.DataContext).ShowAllFilms();
            }
        }

        private void SettingControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue == true)
            {
            }
        }
    }
}

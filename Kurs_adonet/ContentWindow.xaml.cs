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
using System.Windows.Shapes;

namespace Kurs_adonet
{
    /// <summary>
    /// Interaction logic for ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        bool isFirstStart = true;
        List<string> menuList = new List<string> { "Все фильмы", "Мои фильмы", "Настройки", "О проекте" };
        public ContentWindow()
        {
            InitializeComponent();
            DemoItemsListBox.SelectedIndex = 0;
        }

        private void DemoItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFirstStart)
            {
                PageName.Content = menuList[DemoItemsListBox.SelectedIndex];
                BackButton.IsChecked = false;
            }
            isFirstStart = false;
        }
    }
}

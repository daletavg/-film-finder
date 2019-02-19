﻿using System;
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
using MaterialDesignThemes.Wpf;

namespace Kurs_adonet
{
    /// <summary>
    /// Interaction logic for ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        bool isFirstStart = true;
        List<string> menuList = new List<string> { "Все фильмы", "Мои фильмы", "Настройки","Чат", "О проекте" };
        public ContentWindow()
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
                    break;
                default:
                    this.AllFilms.Visibility = Visibility.Hidden;
                    break;
            }
        }
        
    }
}

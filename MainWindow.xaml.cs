using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Security;
using System.Text.RegularExpressions; 


namespace SpriteStomper
{
    public partial class MainWindow : Window
    {
        public void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.FileName = "image";

            dlg.Filter = "Image Files (*.png;*.BMP;*.JPG;*.GIF)|*.png;*.BMP;*.JPG;*.GIF";

            dlg.Multiselect = true;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string[] filename = dlg.FileNames;

               
                canvas1.LoadImage(filename[0]);

                canvas1.GenSheet(filename); 
            }

        }
        public MainWindow()
        {
            InitializeComponent();

        }
    }
}

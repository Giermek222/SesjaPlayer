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
using System.IO;

namespace SesjaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageWindow image_player = new ImageWindow();
        
        
        public MainWindow()
        {
            InitializeComponent();
            DirectoryInfo di = new DirectoryInfo(@"D:/Wladek/repos/Sesja_view/SesjaPlayer/SesjaPlayer/photos");
            List<FileInfo> files = new List<FileInfo>(di.GetFiles());
            di = new DirectoryInfo(@"D:/Wladek/repos/Sesja_view/SesjaPlayer/SesjaPlayer/npc");
            List<FileInfo> npcs = new List<FileInfo>(di.GetFiles());
            Backgrounds.ItemsSource = files;
            Npc_list.ItemsSource = npcs;
            image_player.Show();
            
        }

        private void SetBackGroundImage(string source)
        {
            Image filip = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(source);
            bitmap.EndInit();
            if (bitmap.Width > bitmap.Height)
            {
                double transform_x = image_player.Width / bitmap.Width;
                double transform_y = image_player.Height / bitmap.Height;
                var targetBitmap = new TransformedBitmap(bitmap, new ScaleTransform(transform_x, transform_y));
                filip.Source = targetBitmap;
            }
            else
                filip.Source = bitmap;

            image_player.Content = filip;
        }
        private void SetNPCImage(string source, NPCWindow npc_window)
        {
            Image filip = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(source);
            bitmap.EndInit();
            npc_window.Width = 600;
            npc_window.Height = 800;
            double transform_x = npc_window.Width / bitmap.Width;
            double transform_y = npc_window.Height / bitmap.Height;
            var targetBitmap = new TransformedBitmap(bitmap, new ScaleTransform(transform_x, transform_y));
            filip.Source = targetBitmap;
            npc_window.Content = filip;
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            image_player.Close();
            
        }

        private void Backgrounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileInfo f = Backgrounds.SelectedItem as FileInfo;
            SetBackGroundImage(f.FullName);
        }

        private void Npc_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NPCWindow npc_window = new NPCWindow();
            FileInfo f = Npc_list.SelectedItem as FileInfo;
            SetNPCImage(f.FullName, npc_window);
            npc_window.Show();
        }
    }


}

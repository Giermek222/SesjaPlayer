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
        private ImageWindow image_player;
        
        public MainWindow()
        {
            InitializeComponent();
            image_player = new ImageWindow();
            DirectoryInfo di = new DirectoryInfo(@"D:/Wladek/repos/Sesja_view/SesjaPlayer/SesjaPlayer/photos");
            List<FileInfo> files = new List<FileInfo>(di.GetFiles());
            Backgrounds.ItemsSource = files;
            image_player.Show();
            
        }

        private void SetImage(string source)
        {
            Image filip = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(source);
            bitmap.EndInit();
            double transform_x = image_player.Width / bitmap.Width;
            double transform_y = image_player.Height / bitmap.Height;
            var targetBitmap = new TransformedBitmap(bitmap, new ScaleTransform(transform_x, transform_y));
            filip.Source = targetBitmap;

            image_player.Content = filip;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            image_player.Close();
        }

        private void Backgrounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileInfo f = Backgrounds.SelectedItem as FileInfo;
            SetImage(f.FullName);
        }
    }
}

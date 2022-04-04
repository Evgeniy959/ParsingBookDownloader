using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParsingBookDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FolderBrowserDialog folder;
        List<Book> books;
        public MainWindow()
        {
            InitializeComponent();
            folder = new FolderBrowserDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.Clear();
            SiteToString siteToString = new SiteToString();
            string site = siteToString.GetSite("https://tululu.org/search/?q=" + TB.Text);
            BooksParser parser = new BooksParser();
            books = parser.GetBooks(site);

            foreach (Book x in books)
            {
                LB.Items.Add(x.Name);
                //LB.Items.Add(x.Url);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            folder.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (LB.SelectedIndex < 0)
                return;
            if (folder.SelectedPath.Length == 0 || folder.SelectedPath == null)
            {
                if (folder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
            }
            WebClient client = new WebClient();
            try
            {
                /*string path = folder.SelectedPath + "\\"
                     + books[LB.SelectedIndex].Name + ".txt";*/
                string path = $"{folder.SelectedPath}\\{books[LB.SelectedIndex].Name}.txt";
                client.DownloadFileAsync(new Uri(books[LB.SelectedIndex].Url), path);

            }
            catch (WebException ex)
            {
                //System.Windows.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        async void DowloadAll()
        {
            WebClient client = new WebClient();
            if (folder.SelectedPath.Length == 0 || folder.SelectedPath == null)
            {
                if (folder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
            }
            PB.Value = 0;
            PB.Maximum = books.Count;
            for (int i = 0; i < books.Count; i++)
            {
                string path = $"{folder.SelectedPath}\\{books[i].Name}.txt";
                LB1.Content = books[i].Name;
                await Task.Run(() => client.DownloadFile(new Uri(books[i].Url), path));
                await Task.Run(() => Dispatcher.Invoke(new Action(() => { PB.Value += 1; })));
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                DowloadAll();
            }
            catch (WebException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }

}

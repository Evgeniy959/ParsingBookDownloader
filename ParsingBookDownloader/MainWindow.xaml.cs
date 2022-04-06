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
        string GenerateFilename(string bookName)
        {
            string result = "";
            for (int i = 0; i < bookName.Length; i++)
            {
                if (bookName[i] == '?' || bookName[i] == '\"' || bookName[i] == '|'
                    || bookName[i] == '\\' || bookName[i] == ' ' || bookName[i] == '*'
                    || bookName[i] == '«'
                    || bookName[i] == '»' || bookName[i] == '>' || bookName[i] == '<'
                    || bookName[i] == ':' || bookName[i] == '/' || bookName[i] == '\n')
                    continue;
                result += bookName[i];
            }
            if (result.Length == 0)
                result = "book";
            result += ".txt";
            return result;
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
            Uri url = new Uri(books[LB.SelectedIndex].Url);
            /*if (url == null)
            {
                throw new Exception("Ссылка не существует");
            }*/
            try
            {
                string path = $"{folder.SelectedPath}\\{books[LB.SelectedIndex].Name}.txt";
                client.DownloadFile(url, path);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }

}

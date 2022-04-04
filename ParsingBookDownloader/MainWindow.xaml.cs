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
        List<Book> books;
        public static FolderBrowserDialog folder;
        public MainWindow()
        {
            InitializeComponent();
            books = new List<Book>();
            folder = new FolderBrowserDialog();
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            LB.Items.Clear();
            BookParserTululuDotOrg site = new BookParserTululuDotOrg(TextSearch.Text);

            books = site.GetBooks();

            foreach (Book b in books)
            {
                LB.Items.Add($"{b.Name} ({b.Author})");
            }
        }

        private void ButtonSelectPath(object sender, RoutedEventArgs e)
        {
            folder.ShowDialog();
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            if (LB.SelectedIndex < 0) return;

            if (folder.SelectedPath.Length == 0 || folder.SelectedPath == null)
            {
                if (folder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                { return; }
            }

            DownloadWindow dw = new DownloadWindow(books[LB.SelectedIndex], folder.SelectedPath);
            dw.ShowDialog();
        }

    }

}

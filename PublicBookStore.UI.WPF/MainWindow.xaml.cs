using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;
using PublicBookStore.UI.WPF.DataService;
using PublicBookStore.UI.WPF.Helpers;
using PublicBookStore.UI.WPF.Models;

namespace PublicBookStore.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private List<GenreModel> _genreDataSource;
        private BookStoreService service =new BookStoreService();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

        }



        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var genre = (GenreModel) GenreListBox.SelectedValue;

            if (genre != null)
            {
                BindBooks(genre.GenreId);
            }

        }

        private async void BindGenres()
        {
            try
            {
                var data = await service.GetGenresAsync();
                GenreListBox.DataContext = data;
             
            }
            catch
            {
                // TODO: Log
            }
        }

        private async void BindBooks(int genreId)
        {
            try
            {
                var data = await service.GetBooksAsync(genreId);
           
                BooksListBox.DataContext = data;
           
            }
            catch
            {

                //TODO:Log
            }
        }



        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            BindGenres();
            BindBooks(0);
           
        }

        private void ListBoxBooks_OnSelected(object sender, RoutedEventArgs e)
        {
            this.Title = "Test";
        }

        private async void BooksListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var book = (BookModel)BooksListBox.SelectedValue;

            if (book == null) return;
            BookImage.Source = book.Image;
            BookTitle.Text = book.Title;
            BookAuthor.Text = await book.GetAuthorName();
            BookGenre.Text = await book.GetGenreName();
            BookPrice.Text = $"$ {book.Price.ToString(CultureInfo.InvariantCulture)}"; 
            BookPublished.Text = book.Published.ToString("yyyy-MM-dd");
            BookDetail.Text = book.Description;
        }
    }
}

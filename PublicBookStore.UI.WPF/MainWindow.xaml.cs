using System;
using System.Collections.Generic;
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
        #endregion
        public MainWindow()
        {
            InitializeComponent();
           
        }

        

        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var path = ((sender as ListBox)?.SelectedItem.ToString());

        }

        private async void BindGenres()
        {
            try
            {
                var data = await GetGenres();

                LayoutListBox.DataContext = data;
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
                var data = await GetBooks(genreId);
                BooksListBox.DataContext = data;
            }
            catch
            {

                //TODO:Log
            }
        }

        private static async Task<List<GenreModel>> GetGenres()
        {
            var data = new List<GenreModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigHelper.ApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Response:
                var response = await client.GetAsync("api/genre");
                if (!response.IsSuccessStatusCode) return data;
                var genresJsonData = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<List<GenreModel>>(genresJsonData);
            }

            return data.OrderBy(o => o.Name).ToList();

        }

        private static async Task<List<BookModel>> GetBooks(int genreId)
        {
            var data = new List<BookModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigHelper.ApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Response:
                var response = await client.GetAsync("api/book");
                if (!response.IsSuccessStatusCode) return data;
                var genresJsonData = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<List<BookModel>>(genresJsonData);
            }

            return data.Where(d => genreId <= 0 || d.GenreId == genreId).OrderBy(o => o.Title).ToList();

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
    }
}

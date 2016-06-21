using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            BindGenres();

        }



        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
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

    }
}

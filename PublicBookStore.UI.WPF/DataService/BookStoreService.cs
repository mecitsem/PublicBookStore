using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PublicBookStore.UI.WPF.Helpers;
using PublicBookStore.UI.WPF.Models;

namespace PublicBookStore.UI.WPF.DataService
{
    public class BookStoreService
    {
        #region GENRE METHODS
        public async Task<List<GenreModel>> GetGenresAsync()
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

        public async Task<GenreModel> GetGenreAsync(int genreId)
        {
            var data = new GenreModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigHelper.ApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Response:
                var response = await client.GetAsync("api/genre/" + genreId);
                if (!response.IsSuccessStatusCode) return data;
                var genresJsonData = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<GenreModel>(genresJsonData);
            }

            return data;

        }
        #endregion

        #region AUTHOR METHOD

        public async Task<List<AuthorModel>> GetAuthorsAsync()
        {
            var data = new List<AuthorModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigHelper.ApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Response:
                var response = await client.GetAsync("api/author");
                if (!response.IsSuccessStatusCode) return data;
                var genresJsonData = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<List<AuthorModel>>(genresJsonData);
            }

            return data;

        }

        public async Task<AuthorModel> GetAuthorAsync(int authorId)
        {
            var data = new AuthorModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigHelper.ApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Response:
                var response = await client.GetAsync("api/author/" + authorId);
                if (!response.IsSuccessStatusCode) return data;
                var genresJsonData = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<AuthorModel>(genresJsonData);
            }

            return data;

        }
        #endregion

        #region BOOK METHODS
        public async Task<List<BookModel>> GetBooksAsync(int genreId)
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
        #endregion
    }
}

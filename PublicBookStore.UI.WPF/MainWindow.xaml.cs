using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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

            LayoutListBox.DataContext = GenreDataSource;

        }

        private List<GenreModel> GenreDataSource => _genreDataSource ?? (_genreDataSource = SampleData());

        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private List<GenreModel> SampleData()
        {
            var data = new List<GenreModel>()
            {
                new GenreModel()
                {
                    Id = 1,
                    Name = "Genre1"
                },
                new GenreModel()
                {
                    Id = 2,
                    Name = "Genre2"
                }
            };

            return data;
            
        }
    }
}

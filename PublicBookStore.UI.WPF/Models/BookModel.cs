using System;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;
using PublicBookStore.UI.WPF.Helpers;

namespace PublicBookStore.UI.WPF.Models
{
    public class BookModel : BaseModel
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Published { get; set; }
        public decimal Price { get; set; }

        public BitmapImage Image
        {
            get
            {
                var image = new BitmapImage();
                const int bytesToRead = 100;

                var request = WebRequest.Create(new Uri(ConfigHelper.ApiUri + ImageUrl, UriKind.Absolute));
                request.Timeout = -1;
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    var reader = new BinaryReader(responseStream);
                    var memoryStream = new MemoryStream();

                    var bytebuffer = new byte[bytesToRead];
                    var bytesRead = reader.Read(bytebuffer, 0, bytesToRead);

                    while (bytesRead > 0)
                    {
                        memoryStream.Write(bytebuffer, 0, bytesRead);
                        bytesRead = reader.Read(bytebuffer, 0, bytesToRead);
                    }

                    image.BeginInit();
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    image.StreamSource = memoryStream;
                }
                image.EndInit();

                return image;
            }
        }
    }
}

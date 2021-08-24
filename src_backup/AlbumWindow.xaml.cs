using MetaBrainz.MusicBrainz;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;


namespace MusicBrainzSearcher
{
    /// <summary>
    /// Interaktionslogik für AlbumWindow.xaml
    /// </summary>
    public partial class AlbumWindow : Window
    {
        public AlbumWindow(string AlbumQuery)
        {
            InitializeComponent();
            //GetData() creates a collection of Customer data from a database
            ObservableCollection<Album> albumData = GetData(AlbumQuery);

            //Bind the DataGrid to the customer data
            DG1.DataContext = albumData;
        }

        private ObservableCollection<Album> GetData(string albumQuery)
        {
            var foundAlbums = new ObservableCollection<Album>();
            var q = new Query("BrainQueryAlbum", "0.1", "mailto:milton.waddams@initech.com");
            var albumSearch = q.FindReleases(albumQuery, simple: true);
            foreach (var album in albumSearch.Results)
            {
                Trace.WriteLine($"adding {albumQuery} ...");
                // todo: add artists even when null reference occurs
                try
                {
                    foundAlbums.Add(new Album(album.Item.Title, album.Item.Status, album.Item.EntityType.ToString(), album.Item.Id));
                }
                catch (System.NullReferenceException e)
                {
                    Trace.WriteLine($"could not add {album.Item.Title}, null reference in db ... " + e.StackTrace);
                }
            }
            return foundAlbums;
        }

        //Define the album object
        public class Album
        {
            public string Title { get; set; }
            public string Status { get; set; }
            public string EntityType { get; set; }
            public System.Guid MBID { get; set; }

            public Album(string albumTitle,
                            string albumStatus,
                            string albumType,
                            System.Guid albumMBID)
            {
                this.Title = albumTitle;
                this.Status = albumStatus;
                this.EntityType = albumType;
                this.MBID = albumMBID;
            }

        }

    }
}

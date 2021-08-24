using MetaBrainz.MusicBrainz;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace MusicBrainzSearcher
{
    /// <summary>
    /// Display artists query results
    /// </summary>
    public partial class ArtistsWindow : Window
    {
        public ArtistsWindow(string ArtistQuery)
        {
            InitializeComponent();
            //GetData() creates a collection of Customer data from a database
            ObservableCollection<Artist> artdata = GetData(ArtistQuery);

            //Bind the DataGrid to the customer data
            DG1.DataContext = artdata;
        }

        private ObservableCollection<Artist> GetData(string artistQuery)
        {
            var foundArtists = new ObservableCollection<Artist>();
            var q = new Query("BrainQueryArtist", "0.1", "mailto:milton.waddams@initech.com");
            var artistSearch = q.FindArtists(artistQuery, simple: true);
            foreach (var art in artistSearch.Results)
            {
                Trace.WriteLine($"adding {artistQuery} ...");
                // todo: add artists even when null reference occurs
                try
                {
                    foundArtists.Add(new Artist(art.Item.Name, art.Item.Gender, art.Item.Type, art.Item.BeginArea.Name, art.Item.Id));
                }
                catch (System.NullReferenceException e)
                {
                    Trace.WriteLine($"could not add {art.Item.Name}, null reference in db ... " + e.StackTrace);
                }
            }
            return foundArtists;
        }

        //Define the artist object
        public class Artist
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Type { get; set; }
            public string BeginArea { get; set; }
            public System.Guid MBID { get; set; }

            public Artist(string artistName,
                            string artistGender,
                            string artistType,
                            string artistBeginArea,
                            System.Guid artistMBID)
            {
                this.Name = artistName;
                this.Gender = artistGender;
                this.Type = artistType;
                this.BeginArea = artistBeginArea;
                this.MBID = artistMBID;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MetaBrainz.MusicBrainz;
using System.Diagnostics;

namespace MusicBrainzSearcher
{
    /// <summary>
    /// query for tracks
    /// </summary>
    public partial class TrackWindow : Window
    {
        public TrackWindow(string TrackQuery)
        {
            InitializeComponent();
            //GetData() creates a collection of Customer data from a database
            ObservableCollection<Track> albumData = GetData(TrackQuery);

            //Bind the DataGrid to the customer data
            DG1.DataContext = albumData;
        }

        private ObservableCollection<Track> GetData(string trackQuery)
        {
            var foundTracks = new ObservableCollection<Track>();
            var q = new Query("BrainQueryAlbum", "0.1", "mailto:milton.waddams@initech.com");
            var trackSearch = q.FindReleases(trackQuery, simple: true);
            foreach (var track in trackSearch.Results)
            {
                Trace.WriteLine($"adding {trackQuery} ...");
                // todo: add artists even when null reference occurs
                try
                {
                    foundTracks.Add(new Track(track.Item.Title, track.Item.Disambiguation, track.Item.EntityType.ToString(), track.Item.Id));
                }
                catch (System.NullReferenceException e)
                {
                    Trace.WriteLine($"could not add {track.Item.Title}, null reference in db ... " + e.StackTrace);
                }
            }
            return foundTracks;
        }


        //Define the track object
        public class Track
        {
            public string Title { get; set; }
            public string Disambiguation { get; set; }
            public string EntityType { get; set; }
            public System.Guid MBID { get; set; }

            public Track(string trackTitle,
                            string trackDisambiguation,
                            string trackType,
                            System.Guid trackMBID)
            {
                this.Title = trackTitle;
                this.Disambiguation = trackDisambiguation;
                this.EntityType = trackType;
                this.MBID = trackMBID;
            }

        }

    }
}

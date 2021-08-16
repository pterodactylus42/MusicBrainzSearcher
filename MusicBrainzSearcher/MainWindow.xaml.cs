using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using System.Collections.ObjectModel;
using MetaBrainz.MusicBrainz;

namespace MusicBrainzSearcher
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // is anything entered?
            if ( string.IsNullOrWhiteSpace(SearchTerm.Text) )
            {
                Msg.Content = "please enter any search term";
                return;
            }

            var q = new Query("BrainQuery", "0.1", "mailto:milton.waddams@initech.com");
            // determine search type from entered values
            switch (SearchType.SelectedIndex)
            {
                case 0:
                    ArtistsWindow artistsWindow = new ArtistsWindow(SearchTerm.Text);
                    artistsWindow.Show();
                    break;
                case 1:
                    AlbumWindow albumWindow = new AlbumWindow(SearchTerm.Text);
                    albumWindow.Show();
                    break;
                case 2:
                    TrackWindow trackWindow = new TrackWindow(SearchTerm.Text);
                    trackWindow.Show();
                    break;
                default:
                    Trace.WriteLine("Shoot yourself if this happens");
                    return;
            }
            SearchTerm.Clear();
        }
    }
}

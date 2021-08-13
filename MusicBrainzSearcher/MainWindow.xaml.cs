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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if( !string.IsNullOrWhiteSpace(SearchTerm.Text) && /* sad but */ true )
            {
                Trace.WriteLine("You entered " + SearchTerm.Text );
                var q = new Query("BrainQuery", "0.1", "mailto:milton.waddams@initech.com");
                // TODO: query for whatever type is selected and display
                // 0 = Artist, 1 = Album, 2 = Track
                switch (SearchType.SelectedIndex)
                {
                    case 0:
                        Trace.WriteLine(SearchType.SelectedIndex + " " + SearchType.SelectedItem.ToString());
                        var artistSearch = q.FindArtists(SearchTerm.Text, simple: true);
                        Trace.WriteLine($"found {artistSearch.TotalResults} artists with name {SearchTerm.Text}");
                        SearchResult.Text = $"found {artistSearch.TotalResults} artists with name {SearchTerm.Text}: ";
                        foreach(var art in artistSearch.Results)
                        {
                            Trace.WriteLine(art.Item.Name);
                            SearchResult.Text += $" {art.Item.Name} ;";
                        }
                        break;
                    case 1:
                        Trace.WriteLine(SearchType.SelectedIndex + " " + SearchType.SelectedItem.ToString());
                        var albumSearch = q.FindReleases(SearchTerm.Text, simple: true);
                        Trace.WriteLine($"found {albumSearch.TotalResults} albums with name {SearchTerm.Text}");
                        SearchResult.Text = $"found {albumSearch.TotalResults} albums with name {SearchTerm.Text}: ";
                        foreach (var rec in albumSearch.Results)
                        {
                            Trace.WriteLine(rec.Item.Title);
                            SearchResult.Text += $" {rec.Item.Title} ;";
                        }
                        break;
                    case 2:
                        Trace.WriteLine(SearchType.SelectedIndex + " " + SearchType.SelectedItem.ToString());
                        var trackSearch = q.FindRecordings(SearchTerm.Text, simple: true);
                        Trace.WriteLine($"found {trackSearch.TotalResults} tracks with name {SearchTerm.Text}");
                        SearchResult.Text = $"found {trackSearch.TotalResults} tracks with name {SearchTerm.Text}: ";
                        foreach (var art in trackSearch.Results)
                        {
                            Trace.WriteLine(art.Item.Title);
                            SearchResult.Text += $" {art.Item.Title} ;";
                        }
                        break;
                    default:
                        Trace.WriteLine("Shoot yourself if this happens");
                        return;
                }
                SearchTerm.Clear();
            } else
            {
                Trace.WriteLine("You entered virtually nothing ;-(");
                SearchResult.Text = "You entered virtually nothing ;-(";

            }

        }
    }
}

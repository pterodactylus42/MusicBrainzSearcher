using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Diagnostics;
using MetaBrainz.MusicBrainz;

namespace MusicBrainzSearcher.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        string name;
        int searchType;
        string msgText;

        public string SearchTerm
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => SearchTerm);
                NotifyOfPropertyChange(() => CanSearchMusicBrainz);
            }
        }

        public int SearchTypeCombo
        {
            get { return searchType; }
        }

        public string MsgTextBox
        {
            get { return msgText;  }
            set
            {
                msgText = value;
                NotifyOfPropertyChange(() => MsgTextBox);
            }
        }

        public bool CanSearchMusicBrainz
        {
            get { return !string.IsNullOrWhiteSpace(SearchTerm); }
        }

        public void SearchMusicBrainz()
        {
            // MessageBox.Show(string.Format("You searched for {0}!", SearchTerm));
            // Todo: query db



            if (!string.IsNullOrWhiteSpace(SearchTerm) && /* sad but */ true)
            {
                Trace.WriteLine("You entered " + SearchTerm);
                var q = new Query("BrainQuery", "0.1", "mailto:milton.waddams@initech.com");
                // TODO: query for whatever type is selected and display
                // 0 = Artist, 1 = Album, 2 = Track
                switch (SearchTypeCombo)
                {
                    case 0:
                        Trace.WriteLine("Search type : " + SearchTypeCombo.ToString());
                        var artistSearch = q.FindArtists(SearchTerm, simple: true);
                        Trace.WriteLine($"found {artistSearch.TotalResults} artists with name {SearchTerm}");
                        MsgTextBox = $"found {artistSearch.TotalResults} artists with name {SearchTerm}: ";
                        foreach (var art in artistSearch.Results)
                        {
                            Trace.WriteLine(art.Item.Name);
                            MsgTextBox += $" {art.Item.Name} ;";
                        }
                        break;
                    case 1:
                        Trace.WriteLine("Search type : " + SearchTypeCombo.ToString());
                        var albumSearch = q.FindReleases(SearchTerm, simple: true);
                        Trace.WriteLine($"found {albumSearch.TotalResults} albums with name {SearchTerm}");
                        MsgTextBox = $"found {albumSearch.TotalResults} albums with name {SearchTerm}: ";
                        foreach (var rec in albumSearch.Results)
                        {
                            Trace.WriteLine(rec.Item.Title);
                            MsgTextBox += $" {rec.Item.Title} ;";
                        }
                        break;
                    case 2:
                        Trace.WriteLine("Search type : " + SearchTypeCombo.ToString());
                        var trackSearch = q.FindRecordings(SearchTerm, simple: true);
                        Trace.WriteLine($"found {trackSearch.TotalResults} tracks with name {SearchTerm}");
                        MsgTextBox = $"found {trackSearch.TotalResults} tracks with name {SearchTerm}: ";
                        foreach (var art in trackSearch.Results)
                        {
                            Trace.WriteLine(art.Item.Title);
                            MsgTextBox += $" {art.Item.Title} ;";
                        }
                        break;
                    default:
                        Trace.WriteLine("Shoot yourself if this happens");
                        return;
                }
                SearchTerm = "";
            }
            else
            {
                Trace.WriteLine("You entered virtually nothing ;-(");
                MsgTextBox = "You entered virtually nothing ;-(";

            }


        }
    }
}

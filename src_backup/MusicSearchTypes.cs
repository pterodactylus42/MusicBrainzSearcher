using System.Collections.ObjectModel;

namespace MusicBrainzSearcher
{
    internal class MusicSearchTypes : ObservableCollection<string>
    {
        public MusicSearchTypes()
        {
            Add("Artist");
            Add("Album");
            Add("Track");
        }
    }
}

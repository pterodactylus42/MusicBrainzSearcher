using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

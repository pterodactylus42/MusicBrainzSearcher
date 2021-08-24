using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MusicBrainzSearcher.ViewModels;

namespace MusicBrainzSearcher
{
    class MusicBrainzBootstrapper : BootstrapperBase
    {
        public MusicBrainzBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}

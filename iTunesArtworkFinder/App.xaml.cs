using System.Windows;
using iTunesArtworkFinder.Properties;

namespace iTunesArtworkFinder
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //if (Debugger.IsAttached) Settings.Default.Reset();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();

            base.OnExit(e);
        }

    }
}

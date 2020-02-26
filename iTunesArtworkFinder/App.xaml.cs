using System;
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

            AppDomain.CurrentDomain.UnhandledException += (o, args) => 
                MessageBox.Show($"{((Exception) args.ExceptionObject).Message}",
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                    MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();

            base.OnExit(e);
        }
    }
}

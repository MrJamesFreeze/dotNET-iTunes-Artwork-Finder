using System.Collections.Specialized;
using System.Windows;

namespace iTunesArtworkFinder.Views
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ((INotifyCollectionChanged)this.ItemsControl.Items).CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.ScrollViewer.ScrollToHome();
            }
        }
    }
}

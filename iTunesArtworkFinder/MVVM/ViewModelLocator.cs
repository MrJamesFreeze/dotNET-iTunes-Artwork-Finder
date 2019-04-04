using iTunesArtworkFinder.ViewModels;

namespace iTunesArtworkFinder.MVVM
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => CreateMainViewModel();

        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel();
        }
    }
}
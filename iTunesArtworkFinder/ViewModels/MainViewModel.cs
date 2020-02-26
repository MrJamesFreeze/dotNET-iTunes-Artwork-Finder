using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Input;
using iTunesArtworkFinder.Classes;
using iTunesArtworkFinder.MVVM;
using iTunesArtworkFinder.Properties;
using iTunesArtworkFinder.ViewModels.Base;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace iTunesArtworkFinder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SearchArtworkCommand { get; }
        public ICommand DownloadArtworkCommand { get; }

        public KeyValuePair<string, string>[] Entities { get; } =
        {
            new KeyValuePair<string, string>("tvSeason", "TV Show"),
            new KeyValuePair<string, string>("movie", "Movie"),
            new KeyValuePair<string, string>("ebook", "iBook"),
            new KeyValuePair<string, string>("album", "Album"),
            new KeyValuePair<string, string>("software", "App (iPhone or Universal)"),
            new KeyValuePair<string, string>("iPadSoftware", "App (iPad)"),
            new KeyValuePair<string, string>("macSoftware", "App (macOS)"),
            new KeyValuePair<string, string>("audiobook", "Audiobook"),
            new KeyValuePair<string, string>("podcast", "Podcast"),
            new KeyValuePair<string, string>("musicVideo", "Music Video"),
            new KeyValuePair<string, string>("id", "Apple ID (Movie)"),
            new KeyValuePair<string, string>("idAlbum", "Apple ID (Album)"),
            new KeyValuePair<string, string>("shortFilm", "Short Film")
        };

        public KeyValuePair<string, string>[] Countries { get; } =
        {
            new KeyValuePair<string, string>("ae", "United Arab Emirates"),
            new KeyValuePair<string, string>("ag", "Antigua and Barbuda"),
            new KeyValuePair<string, string>("ai", "Anguilla"),
            new KeyValuePair<string, string>("al", "Albania"),
            new KeyValuePair<string, string>("am", "Armenia"),
            new KeyValuePair<string, string>("ao", "Angola"),
            new KeyValuePair<string, string>("ar", "Argentina"),
            new KeyValuePair<string, string>("at", "Austria"),
            new KeyValuePair<string, string>("au", "Australia"),
            new KeyValuePair<string, string>("az", "Azerbaijan"),
            new KeyValuePair<string, string>("bb", "Barbados"),
            new KeyValuePair<string, string>("be", "Belgium"),
            new KeyValuePair<string, string>("bf", "Burkina-Faso"),
            new KeyValuePair<string, string>("bg", "Bulgaria"),
            new KeyValuePair<string, string>("bh", "Bahrain"),
            new KeyValuePair<string, string>("bj", "Benin"),
            new KeyValuePair<string, string>("bm", "Bermuda"),
            new KeyValuePair<string, string>("bn", "Brunei Darussalam"),
            new KeyValuePair<string, string>("bo", "Bolivia"),
            new KeyValuePair<string, string>("br", "Brazil"),
            new KeyValuePair<string, string>("bs", "Bahamas"),
            new KeyValuePair<string, string>("bt", "Bhutan"),
            new KeyValuePair<string, string>("bw", "Botswana"),
            new KeyValuePair<string, string>("by", "Belarus"),
            new KeyValuePair<string, string>("bz", "Belize"),
            new KeyValuePair<string, string>("ca", "Canada"),
            new KeyValuePair<string, string>("cg", "Democratic Republic of the Congo"),
            new KeyValuePair<string, string>("ch", "Switzerland"),
            new KeyValuePair<string, string>("cl", "Chile"),
            new KeyValuePair<string, string>("cn", "China"),
            new KeyValuePair<string, string>("co", "Colombia"),
            new KeyValuePair<string, string>("cr", "Costa Rica"),
            new KeyValuePair<string, string>("cv", "Cape Verde"),
            new KeyValuePair<string, string>("cy", "Cyprus"),
            new KeyValuePair<string, string>("cz", "Czech Republic"),
            new KeyValuePair<string, string>("de", "Germany"),
            new KeyValuePair<string, string>("dk", "Denmark"),
            new KeyValuePair<string, string>("dm", "Dominica"),
            new KeyValuePair<string, string>("do", "Dominican Republic"),
            new KeyValuePair<string, string>("dz", "Algeria"),
            new KeyValuePair<string, string>("ec", "Ecuador"),
            new KeyValuePair<string, string>("ee", "Estonia"),
            new KeyValuePair<string, string>("eg", "Egypt"),
            new KeyValuePair<string, string>("es", "Spain"),
            new KeyValuePair<string, string>("fi", "Finland"),
            new KeyValuePair<string, string>("fj", "Fiji"),
            new KeyValuePair<string, string>("fm", "Federated States of Micronesia"),
            new KeyValuePair<string, string>("fr", "France"),
            new KeyValuePair<string, string>("gb", "United Kingdom"),
            new KeyValuePair<string, string>("gd", "Grenada"),
            new KeyValuePair<string, string>("gh", "Ghana"),
            new KeyValuePair<string, string>("gm", "Gambia"),
            new KeyValuePair<string, string>("gr", "Greece"),
            new KeyValuePair<string, string>("gt", "Guatemala"),
            new KeyValuePair<string, string>("gw", "Guinea Bissau"),
            new KeyValuePair<string, string>("gy", "Guyana"),
            new KeyValuePair<string, string>("hk", "Hong Kong"),
            new KeyValuePair<string, string>("hn", "Honduras"),
            new KeyValuePair<string, string>("hr", "Croatia"),
            new KeyValuePair<string, string>("hu", "Hungary"),
            new KeyValuePair<string, string>("id", "Indonesia"),
            new KeyValuePair<string, string>("ie", "Ireland"),
            new KeyValuePair<string, string>("il", "Israel"),
            new KeyValuePair<string, string>("in", "India"),
            new KeyValuePair<string, string>("is", "Iceland"),
            new KeyValuePair<string, string>("it", "Italy"),
            new KeyValuePair<string, string>("jm", "Jamaica"),
            new KeyValuePair<string, string>("jo", "Jordan"),
            new KeyValuePair<string, string>("jp", "Japan"),
            new KeyValuePair<string, string>("ke", "Kenya"),
            new KeyValuePair<string, string>("kg", "Krygyzstan"),
            new KeyValuePair<string, string>("kh", "Cambodia"),
            new KeyValuePair<string, string>("kn", "Saint Kitts and Nevis"),
            new KeyValuePair<string, string>("kr", "South Korea"),
            new KeyValuePair<string, string>("kw", "Kuwait"),
            new KeyValuePair<string, string>("ky", "Cayman Islands"),
            new KeyValuePair<string, string>("kz", "Kazakhstan"),
            new KeyValuePair<string, string>("la", "Laos"),
            new KeyValuePair<string, string>("lb", "Lebanon"),
            new KeyValuePair<string, string>("lc", "Saint Lucia"),
            new KeyValuePair<string, string>("lk", "Sri Lanka"),
            new KeyValuePair<string, string>("lr", "Liberia"),
            new KeyValuePair<string, string>("lt", "Lithuania"),
            new KeyValuePair<string, string>("lu", "Luxembourg"),
            new KeyValuePair<string, string>("lv", "Latvia"),
            new KeyValuePair<string, string>("md", "Moldova"),
            new KeyValuePair<string, string>("mg", "Madagascar"),
            new KeyValuePair<string, string>("mk", "Macedonia"),
            new KeyValuePair<string, string>("ml", "Mali"),
            new KeyValuePair<string, string>("mn", "Mongolia"),
            new KeyValuePair<string, string>("mo", "Macau"),
            new KeyValuePair<string, string>("mr", "Mauritania"),
            new KeyValuePair<string, string>("ms", "Montserrat"),
            new KeyValuePair<string, string>("mt", "Malta"),
            new KeyValuePair<string, string>("mu", "Mauritius"),
            new KeyValuePair<string, string>("mw", "Malawi"),
            new KeyValuePair<string, string>("mx", "Mexico"),
            new KeyValuePair<string, string>("my", "Malaysia"),
            new KeyValuePair<string, string>("mz", "Mozambique"),
            new KeyValuePair<string, string>("na", "Namibia"),
            new KeyValuePair<string, string>("ne", "Niger"),
            new KeyValuePair<string, string>("ng", "Nigeria"),
            new KeyValuePair<string, string>("ni", "Nicaragua"),
            new KeyValuePair<string, string>("nl", "Netherlands"),
            new KeyValuePair<string, string>("np", "Nepal"),
            new KeyValuePair<string, string>("no", "Norway"),
            new KeyValuePair<string, string>("nz", "New Zealand"),
            new KeyValuePair<string, string>("om", "Oman"),
            new KeyValuePair<string, string>("pa", "Panama"),
            new KeyValuePair<string, string>("pe", "Peru"),
            new KeyValuePair<string, string>("pg", "Papua New Guinea"),
            new KeyValuePair<string, string>("ph", "Philippines"),
            new KeyValuePair<string, string>("pk", "Pakistan"),
            new KeyValuePair<string, string>("pl", "Poland"),
            new KeyValuePair<string, string>("pt", "Portugal"),
            new KeyValuePair<string, string>("pw", "Palau"),
            new KeyValuePair<string, string>("py", "Paraguay"),
            new KeyValuePair<string, string>("qa", "Qatar"),
            new KeyValuePair<string, string>("ro", "Romania"),
            new KeyValuePair<string, string>("ru", "Russia"),
            new KeyValuePair<string, string>("sa", "Saudi Arabia"),
            new KeyValuePair<string, string>("sb", "Soloman Islands"),
            new KeyValuePair<string, string>("sc", "Seychelles"),
            new KeyValuePair<string, string>("se", "Sweden"),
            new KeyValuePair<string, string>("sg", "Singapore"),
            new KeyValuePair<string, string>("si", "Slovenia"),
            new KeyValuePair<string, string>("sk", "Slovakia"),
            new KeyValuePair<string, string>("sl", "Sierra Leone"),
            new KeyValuePair<string, string>("sn", "Senegal"),
            new KeyValuePair<string, string>("sr", "Suriname"),
            new KeyValuePair<string, string>("st", "Sao Tome e Principe"),
            new KeyValuePair<string, string>("sv", "El Salvador"),
            new KeyValuePair<string, string>("sz", "Swaziland"),
            new KeyValuePair<string, string>("tc", "Turks and Caicos Islands"),
            new KeyValuePair<string, string>("td", "Chad"),
            new KeyValuePair<string, string>("th", "Thailand"),
            new KeyValuePair<string, string>("tj", "Tajikistan"),
            new KeyValuePair<string, string>("tm", "Turkmenistan"),
            new KeyValuePair<string, string>("tn", "Tunisia"),
            new KeyValuePair<string, string>("tr", "Turkey"),
            new KeyValuePair<string, string>("tt", "Republic of Trinidad and Tobago"),
            new KeyValuePair<string, string>("tw", "Taiwan"),
            new KeyValuePair<string, string>("tz", "Tanzania"),
            new KeyValuePair<string, string>("ua", "Ukraine"),
            new KeyValuePair<string, string>("ug", "Uganda"),
            new KeyValuePair<string, string>("us", "United States of America"),
            new KeyValuePair<string, string>("uy", "Uruguay"),
            new KeyValuePair<string, string>("uz", "Uzbekistan"),
            new KeyValuePair<string, string>("vc", "Saint Vincent and the Grenadines"),
            new KeyValuePair<string, string>("ve", "Venezuela"),
            new KeyValuePair<string, string>("vg", "British Virgin Islands"),
            new KeyValuePair<string, string>("vn", "Vietnam"),
            new KeyValuePair<string, string>("ye", "Yemen"),
            new KeyValuePair<string, string>("za", "South Africa"),
            new KeyValuePair<string, string>("zw", "Zimbabwe")
        };

        private string _selectedEntity = Settings.Default.SelectedEntity != string.Empty
            ? Settings.Default.SelectedEntity
            : "tvSeason";

        private string _selectedCountry = Settings.Default.SelectedCountry != string.Empty
            ? Settings.Default.SelectedCountry
            : "us";

        private string _searchText = string.Empty;

        private string _initialFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public string SelectedEntity
        {
            get => this._selectedEntity;
            set
            {
                this._selectedEntity = value;
                Settings.Default.SelectedEntity = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCountry
        {
            get => this._selectedCountry;
            set {
                this._selectedCountry = value;
                Settings.Default.SelectedCountry = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => this._searchText;
            set
            {
                this._searchText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Artwork> Results { get; set; } = new ObservableCollection<Artwork>();

        public bool NoResults => this.Results.Count == 0;


        public MainViewModel()
        {
            Array.Sort(this.Countries, (x, y) => string.Compare(x.Value, y.Value, StringComparison.CurrentCulture));

            this.SearchArtworkCommand = new RelayCommand(SearchArtworksExecute, SearchArtworksCanExecute);
            this.DownloadArtworkCommand = new RelayCommand(DownloadArtwork);
        }

        private void SearchArtworksExecute()
        {
            string url;
            string entity = this.SelectedEntity;
            string country = this.SelectedCountry;
            string searchText = this.SearchText;

            switch (entity)
            {
                case "shortFilm":
                    url = $"https://itunes.apple.com/search?term={Uri.EscapeUriString(searchText)}&country={country}&entity=movie&attribute=shortFilmTerm";
                    break;
                case "id":
                case "idAlbum":
                    url = $"https://itunes.apple.com/lookup?id={Uri.EscapeUriString(searchText)}&country={country}";
                    break;
                default:
                    url = $"https://itunes.apple.com/search?term={Uri.EscapeUriString(searchText)}&country={country}&entity={entity}";
                    break;
            }
            url += "&limit=25";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            JObject json;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = JObject.Parse(reader.ReadToEnd());
            }

            this.Results.Clear();
            foreach (var result in (JArray)json["results"])
            {
                Artwork item = new Artwork
                {
                    Title = (this.SelectedEntity == "movie") ? (string) result["trackName"] : (string) result["collectionName"],
                    Url = ((string) result["artworkUrl100"]).Replace("100x100", "600x600"),
                    UrlHiRes = ((string) result["artworkUrl100"]).Replace("100x100bb", "100000x100000-999")
                };

                switch (this.SelectedEntity)
                {
                    case "musicVideo":
                        item.Title = $"{result["trackName"]} (by {result["artistName"]})";
                        break;
                    case "tvSeason":
                        item.Title = $"{result["collectionName"]}";
                        break;
                    case "movie":
                    case "id":
                    case "shortFilm":
                        item.Title = (result["trackName"] != null) ? $"{result["trackName"]}" : $"{result["collectionName"]}";
                        break;
                    case "ebook":
                        item.Title = $"{result["trackName"]} (by {result["artistName"]})";
                        break;
                    case "album":
                    case "idAlbum":
                        item.Title = $"{result["collectionName"]} (by {result["artistName"]})";
                        break;
                    case "audiobook":
                        item.Title = $"{result["collectionName"]} (by {result["artistName"]})";
                        break;
                    case "podcast":
                        item.Title = $"{result["collectionName"]} (by {result["artistName"]})";
                        break;
                    case "software":
                    case "iPadSoftware":
                    case "macSoftware":
                        item.Title = $"{result["trackName"]}";
                        item.Url = (string) result["artworkUrl512"];
                        item.UrlHiRes = ((string) result["artworkUrl512"]).Replace("512x512bb", "1024x1024bb");
                        break;
                    default:
                        break;
                }

                this.Results.Add(item);
            }

            OnPropertyChanged(nameof(this.NoResults));

        }

        private bool SearchArtworksCanExecute()
        {
            return this.SearchText != string.Empty;
        }

        private void DownloadArtwork(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = UniqueFilename.NextAvailableFilename( $"{this._initialFolder}\\artwork.jpg"),
                Filter = "JPEG File Interchange Format (*.jpg)|*.jpg",
                FilterIndex = 0,
                InitialDirectory = this._initialFolder
            };
            if (saveFileDialog.ShowDialog() != true) return;

            this._initialFolder = Path.GetDirectoryName(saveFileDialog.FileName);
            
            using (WebClient client = new WebClient())
            {
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
                client.DownloadFileAsync(new Uri((string) parameter), saveFileDialog.FileName);
            }
        }

        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }
    }
}
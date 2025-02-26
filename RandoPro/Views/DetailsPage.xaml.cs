using System.Windows.Input;

namespace RandoPro
{
    [QueryProperty(nameof(Detail), "Detail")]

    public partial class DetailsPage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        Rando detail;
        public Rando Detail
        {
            get => detail;
            set
            {
                Detail = value;
                OnPropertyChanged();
            }
        }
        public DetailsPage(RandoDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
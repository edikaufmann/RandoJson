namespace RandoPro.ViewModel;

[QueryProperty(nameof(Rando), "Rando")]
public partial class RandoDetailsViewModel : BaseViewModel, IQueryAttributable
{
    IMap map;
    public RandoDetailsViewModel(IMap map)
    {
        this.map = map;
    }

    //public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url)); //edi

    [ObservableProperty]
    Rando rando;

    [RelayCommand]
    void TapPhotos(string Photos)
    {}

        [RelayCommand]
    void TapMap(string Map)
    {}

            [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await map.OpenAsync(Rando.lat, Rando.lon, new MapLaunchOptions
                {
                    Name = Rando.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }
    

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Rando = query["Rando"] as Rando;
    }


}
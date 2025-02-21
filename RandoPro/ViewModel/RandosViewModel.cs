using RandoPro.Services;

namespace RandoPro.ViewModel;

public partial class RandosViewModel : BaseViewModel
{
    public ObservableCollection<Rando> Randos { get; } = new();
    //public bool IsRefreshing1 { get => isRefreshing; set => isRefreshing = value; }

    RandoService randoService;
    IConnectivity connectivity;
    IGeolocation geolocation;
    public RandosViewModel(RandoService randoService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Rando Finder";
        this.randoService = randoService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [RelayCommand]
    async Task GoToDetails(Rando rando)
    {
        if (rando == null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Rando", rando }
        });
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetRandosAsync()
    {
        if (IsBusy)
           return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var randos = await randoService.GetRandos();

            if(Randos.Count != 0)
                Randos.Clear();

            foreach(var rando in randos)
                Randos.Add(rando);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get randos: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }

    [RelayCommand]
    async Task GetClosestRando()
    {
        if (IsBusy || Randos.Count == 0)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            // Find closest rando to us
            var first = Randos.OrderBy(m => location.CalculateDistance(
                new Location(m.lat, m.lon), DistanceUnits.Miles))
                .FirstOrDefault();

            await Shell.Current.DisplayAlert("", first.Name + " " +
                first.Area, "OK");

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}

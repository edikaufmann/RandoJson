using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RandoPro.ViewModel;

[QueryProperty(nameof(Rando), "Rando")]
public partial class RandoDetailsViewModel : BaseViewModel
{
    IMap map;
    public RandoDetailsViewModel(IMap map)
    {
        this.map = map;
    }

    [ObservableProperty]
    Rando rando;

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
}

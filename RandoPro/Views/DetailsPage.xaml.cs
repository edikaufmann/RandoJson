using RandoPro.Models;
using System.Windows.Input;

namespace RandoPro;
public partial class DetailsPage : ContentPage
{
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public DetailsPage(RandoDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }    
}

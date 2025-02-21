using RandoPro.Models;

namespace RandoPro;
public partial class DetailsPage : ContentPage
{
    public DetailsPage(RandoDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }    
}

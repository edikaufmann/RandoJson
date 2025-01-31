namespace RandoPro.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(RandoDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
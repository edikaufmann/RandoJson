namespace RandoPro.Views;

public partial class MainPage : ContentPage
{
	public MainPage(RandosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}


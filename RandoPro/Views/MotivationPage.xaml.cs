using System.Windows.Input;

namespace RandoPro.Views
{
    public partial class MotivationPage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public MotivationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}

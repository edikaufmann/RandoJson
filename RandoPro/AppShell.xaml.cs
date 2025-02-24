using System.Windows.Input;
using Microsoft.Win32;
using RandoPro.Views;

namespace RandoPro;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    //public ICommand SiteCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public ICommand SiteCommand => new Command<string>(async (url) => {
        if (url.StartsWith("http"))
        {
            await Launcher.OpenAsync(url);
        }
        else
        {
            await Shell.Current.GoToAsync(url);
        }
    });

    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
        BindingContext = this;
    }

    void RegisterRoutes()
    {
        Routes.Add("mainpage", typeof(Views.MainPage));
        //Routing.RegisterRoute("DetailsPage", typeof(DetailsPage));
        Routes.Add("DetailsPage", typeof(DetailsPage));
        Routes.Add("MotivationPage", typeof(Views.MotivationPage));
             

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }

    //protected override void OnNavigating(ShellNavigatingEventArgs args)
    //{
    //    base.OnNavigating(args);

    //    // Cancel any back navigation
    //    //if (e.Source == ShellNavigationSource.Pop)
    //    //{
    //    //    e.Cancel();
    //    //}
    //}

    //protected override void OnNavigated(ShellNavigatedEventArgs args)
    //{
    //    base.OnNavigated(args);

    //    // Perform required logic
    //}
}

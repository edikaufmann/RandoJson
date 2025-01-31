using RandoPro.Models;

namespace RandoPro.Controls
{
    public class RandoSearchHandler : SearchHandler
    {
        public IList<Rando> Randos { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Randos
                    .Where(rando => rando.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<Rando>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            Rando rando = item as Rando;
            string v = GetNavigationTarget();
            string navigationTarget = v;

            if (navigationTarget.Equals("DetailsPage"))
            {
                // Navigate, passing a string
                await Shell.Current.GoToAsync($"{navigationTarget}?name={((Rando)item).Name}");
            }
            else
            {
                string lowerCasePropertyName = navigationTarget.Replace("details", string.Empty);
                // Capitalise the property name
                string propertyName = char.ToUpper(lowerCasePropertyName[0]) + lowerCasePropertyName.Substring(1);
                
                var navigationParameters = new Dictionary<string, object>
                {
                    { propertyName, rando }
                };

                // Navigate, passing an object
                await Shell.Current.GoToAsync($"{navigationTarget}", navigationParameters);
            }
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}

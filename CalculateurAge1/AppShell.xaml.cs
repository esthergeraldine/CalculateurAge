
using CalculateurAge1.Views;

namespace CalculateurAge1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Déclare la route : sans cette ligne, GoToAsync lève une exception "route inconnue".
        Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
    }
}

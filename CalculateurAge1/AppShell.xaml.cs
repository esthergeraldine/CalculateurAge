
using CalculateurAge .Views;
namespace CalculateurAge1

public AppShell()
{
    InitializeComponent();
    // Declare la route : sans cette ligne, GoToAsync
    // leve une exception "route inconnue".
    Routing.RegisterRoute(nameof(ResultatPage),
        typeof(ResultatPage));
}
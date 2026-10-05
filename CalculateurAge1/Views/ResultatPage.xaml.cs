using CalculateurAge1.ViewModels;

namespace CalculateurAge1.Views;

[QueryProperty(nameof(Info), "Info")]
public partial class ResultatPage : ContentPage
{
    // Reçoit l'objet envoyé par le ViewModel et le donne aux bindings
    public ResultatInfo Info
    {
        set => BindingContext = value;
    }

    public ResultatPage() => InitializeComponent();

    private async void OnRetourClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}
using CalculateurAge1.views;

namespace CalculateurAge1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlert("Erreur", "Entrez un nom", "OK");
            return;
        }

        DateTime d = (DateTime)pickerDate.Date;
        int age = DateTime.Today.Year - d.Year;
        if (d.Date > DateTime.Today.AddYears(-age)) age--;

        lblResultat.Text = $"{entryNom.Text}, vous avez {age} ans";
        lblResultat.IsVisible = true;
    }
}
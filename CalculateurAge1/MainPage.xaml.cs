using CalculateurAge1.views;

namespace CalculateurAge1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }


        private async void OnCalculerClicked(object sender, EventArgs e)
        {
            // Validation : on refuse un nom vide.
            if (string.IsNullOrWhiteSpace(entryNom.Text))
            {
                await DisplayAlert("Erreur", "Entrez un nom", "OK");
                return;
            }

            // DatePicker.Date n'est pas nullable en .NET MAUI : on récupère directement la date.
            DateTime d = pickerDate.Date;
            int age = DateTime.Today.Year - d.Year;
            // Si 1 anniversaire n est pas encore passe cette annee,
            // on retire une annee.
            if (d.Date > DateTime.Today.AddYears(-age)) age--;

            // On escrit DIRECTEMENT dans les controles : c est
            // precisement ce que le MVVM va supprimer.
            await Shell.Current.GoToAsync(
                $"{nameof(ResultatPage)}?nom={entryNom.Text}&age={age}");
        }

    }
}
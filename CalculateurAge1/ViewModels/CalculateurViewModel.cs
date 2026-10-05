using CalculateurAge1.Views;

namespace CalculateurAge1.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
            {
                OnPropertyChanged(nameof(ErreurDate));
                OnPropertyChanged(nameof(ErreurVisible));
                CalculerCommand.Rafraichir();
            }
        }
    }

    public bool DateFuture => DateNaissance.Date > DateTime.Today;
    public bool ErreurVisible => DateFuture;
    public string ErreurDate => DateFuture
        ? "La date de naissance ne peut pas être dans le futur."
        : "";

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            async () => await Calculer(),
            () => !string.IsNullOrWhiteSpace(Nom) && !DateFuture);

        EffacerCommand = new RelayCommand(Effacer);
    }

    private async Task Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        int jours = JoursAvantAnniversaire();

        var info = new ResultatInfo
        {
            Resultat = $"{Nom}, vous avez {age} ans",
            Message = age >= 18 ? "Majeur" : "Mineur",
            Anniversaire = jours == 0
                ? "Aujourd'hui, c'est votre anniversaire !"
                : $"Prochain anniversaire dans {jours} jour(s)"
        };

        // On envoie l'objet à ResultatPage (pas de texte dans l'URL)
        await Shell.Current.GoToAsync(nameof(ResultatPage),
            new Dictionary<string, object> { { "Info", info } });
    }

    private int JoursAvantAnniversaire()
    {
        DateTime aujourdhui = DateTime.Today;
        DateTime prochain = AnniversaireEnAnnee(aujourdhui.Year);

        if (prochain < aujourdhui)
            prochain = AnniversaireEnAnnee(aujourdhui.Year + 1);

        return (prochain - aujourdhui).Days;
    }

    private DateTime AnniversaireEnAnnee(int annee)
    {
        int mois = DateNaissance.Month;
        int jour = DateNaissance.Day;

        if (mois == 2 && jour == 29 && !DateTime.IsLeapYear(annee))
            jour = 28;

        return new DateTime(annee, mois, jour);
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
    }
}
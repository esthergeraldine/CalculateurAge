namespace CalculateurAge1.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private string _message = "";
    private bool _resultatVisible;

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
                // Fonctionnalité 3 : on prévient la vue que l'erreur a peut-être changé
                OnPropertyChanged(nameof(ErreurDate));
                OnPropertyChanged(nameof(ErreurVisible));
                CalculerCommand.Rafraichir();
            }
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    // Fonctionnalité 1 : "Majeur" ou "Mineur"
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // Fonctionnalité 3 : date future refusée
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
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom) && !DateFuture);

        // Fonctionnalité 2 : Effacer
        EffacerCommand = new RelayCommand(Effacer);
    }

    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";
        ResultatVisible = true;
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        ResultatVisible = false;
    }
}
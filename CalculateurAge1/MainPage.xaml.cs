using CalculateurAge1.ViewModels;

namespace CalculateurAge1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new CalculateurViewModel();
    }
}
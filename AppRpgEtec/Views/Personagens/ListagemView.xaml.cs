using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views;

public partial class PersonagensListagemView : ContentPage
{
	ListagemPersonagemViewModel viewModel;

	public PersonagensListagemView()
	{
		InitializeComponent();

		viewModel = new ListagemPersonagemViewModel();
		BindingContext = viewModel;
		Title = "Personagens - App Rpg Etec";
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_ = viewModel.ObterPersonagens();
    }
}
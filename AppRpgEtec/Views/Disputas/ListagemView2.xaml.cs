using AppRpgEtec.ViewModels.Disputas;

namespace AppRpgEtec.Views.Disputas;

public partial class ListagemView2 : ContentPage
{
	DisputaViewModel viewModel;
	public ListagemView2()
	{
		InitializeComponent();

		viewModel = new DisputaViewModel();
		BindingContext = viewModel;
	}
}
using AppRpgEtec.Views;
using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));

            //Routing.RegisterRoute(nameof(PersonagensListagemView), typeof(PersonagensListagemView));

            //   viewModel = new AppShellViewModel();
            //     BindingContext = viewModel;
        }
    }
}

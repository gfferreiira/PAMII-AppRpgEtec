using AppRpgEtec.Views;
using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CadastroPersonagemView), typeof(CadastroPersonagemView));
            Routing.RegisterRoute(nameof(PersonagensListagemView), typeof(PersonagensListagemView));

            //   viewModel = new AppShellViewModel();
            //     BindingContext = viewModel;
        }
    }
}

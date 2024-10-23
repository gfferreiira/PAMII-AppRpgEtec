using AppRpgEtec.Views;
using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            string login = Preferences.Get("UsuarioUsername", string.Empty);
            lblLogin.Text = login;


            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));

            //Routing.RegisterRoute(nameof(PersonagensListagemView), typeof(PersonagensListagemView));

            //   viewModel = new AppShellViewModel();
            //     BindingContext = viewModel;
        }
    }
}

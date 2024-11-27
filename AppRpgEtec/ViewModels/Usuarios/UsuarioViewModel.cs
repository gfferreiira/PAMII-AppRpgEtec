using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using AppRpgEtec.Views;
using AppRpgEtec.Views.Disputas;
using AppRpgEtec.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public partial class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _uService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }



        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());

        }

        #region AtributosPropriedades
        private string login = string.Empty;
        private string senha = string.Empty;

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
       

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Métodos

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;
                Usuario uAutenticado = await _uService.PostAutenticarUsuarioAsync(u);
                if (!string.IsNullOrEmpty(uAutenticado.Token)) /// COLOCAR O "!" ANTES, POIS NÃO ESTAVA FUNCIONANDO
                {
                    string mensagem = $"Bem Vindo {u.Username}";

                 
                    Preferences.Set("UsuarioToken", uAutenticado.Token);
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);


                    _isCheckingLocation = true;
                    _cancelTokenSource = new CancellationTokenSource();
                    GeolocationRequest request = 
                        new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    Location location= await Geolocation
                        .Default.GetLocationAsync(request, _cancelTokenSource.Token);


                    Usuario uLoc = new Usuario();
                    uLoc.Id = uAutenticado.Id;
                    uLoc.Latitude = location.Latitude;
                    uLoc.Longitude = location.Longitude;

                    UsuarioService uServiceLoc = new UsuarioService(uAutenticado.Token);
                 //   await uServiceLoc.PutAtualizarLocalizacaoAsync(uLoc);

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "OK");

                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await Application.Current.MainPage
                           .DisplayAlert("Informação", "Dados Incorretos", "OK");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + "Detalhes:" + ex.InnerException, "OK");


            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado != null)
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} Registrado com sucesso";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "OK");

                    await Application.Current.MainPage.
                            Navigation.PushAsync(new PersonagensListagemView());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }


        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage.
                    Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message, "Detalhes" + ex.InnerException, "OK");

            }
        }


        #endregion

    }
}


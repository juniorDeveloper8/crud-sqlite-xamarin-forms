using Crud_Sqlite.View;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Crud_Sqlite.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _correo;
        private string _psw;
        private bool _isBusy;

        public string Correo
        {
            get { return _correo; }
            set { _correo = value; OnPropertyChanged(); }
        }

        public string Psw
        {
            get { return _psw; }
            set { _psw = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand IniciarSesionCommand { get; }

        public event EventHandler<string> DisplayAlertRequested;

        public LoginViewModel()
        {
            IniciarSesionCommand = new Command(async () => await IniciarSesion());
        }

        private async Task IniciarSesion()
        {
            if (_isBusy == true)
            {
                if (IsBusy)
                    return;

                if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Psw))
                {
                    OnDisplayAlertRequested("Por favor, ingrese el correo electrónico y la contraseña.");
                    return;
                }
            }
            else
            {

                IsBusy = true;

                bool loginExitoso = await ValidarCredenciales();

                IsBusy = false;

                if (loginExitoso)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new HomeView());
                }
                else
                {
                    DisplayAlertRequested?.Invoke(this, "Correo electrónico o contraseña incorrectos. Por favor, inténtelo de nuevo.");
                }
            }

        }

        private async Task<bool> ValidarCredenciales()
        {
            await Task.Delay(1000);

            if (IsBusy == true)
            {
                return true;
            }
            else
            {

                return false;
            }

            // Aquí deberías implementar la lógica de validación de credenciales
            // Por ahora, devolveré siempre false para simular un fallo en la validación
        }

        private void OnDisplayAlertRequested(string message)
        {
            DisplayAlertRequested?.Invoke(this, message);
        }
    }
}




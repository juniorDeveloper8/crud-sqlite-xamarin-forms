using Crud_Sqlite.ViewModels;
using Xamarin.Forms;

namespace Crud_Sqlite.View
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
            (BindingContext as LoginViewModel).DisplayAlertRequested += HandleDisplayAlertRequested;
        }

        private async void HandleDisplayAlertRequested(object sender, string message)
        {
            await DisplayAlert("Alerta", message, "OK");
        }
    }
}

/**
 * roberth@gmail.com
 */
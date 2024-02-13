using System;
using System.IO;
using System.Threading.Tasks;
using Crud_Sqlite.Service;
using Crud_Sqlite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crud_Sqlite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly HomeViewModel _viewModel;

        public HomeView()
        {
            InitializeComponent();
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "deberUno.db3");

            _clienteRepository = new ClienteService(dbPath);
            _viewModel = new HomeViewModel(_clienteRepository, Navigation);
            BindingContext = _viewModel;

            _viewModel.DisplayAlertRequested += async (sender, message) =>
            {
                await DisplayAlert("Alerta", message, "OK");
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel != null)
            {
                await _viewModel.CargarClientes(); // Recargar la lista de clientes cada vez que se muestra la vista
            }
        }

        private async Task IrAAgregarCliente()
        {
            var agregarClienteView = new AgregarClienteView(_clienteRepository);
            agregarClienteView.ClienteAgregado += async (s, args) =>
            {
                await _viewModel.CargarClientes(); // Llamar al método CargarClientes del _viewModel
            };
            await Navigation.PushAsync(agregarClienteView);
        }

        private async void OnToolbarItemClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Opciones", "Cancelar", null, "Agregar Cliente", "Editar Cliente", "Eliminar Cliente", "Salir");
            switch (action)
            {
                case "Agregar Cliente":
                    await IrAAgregarCliente();
                    break;
                case "Editar Cliente":
                    await EditarCliente();
                    break;
                case "Eliminar Cliente":
                    await EliminarCliente();
                    break;
                //case "Salir":
                //    await Navigation.PushAsync(new LoginView());
                //    break;
            }
        }

        private async Task EditarCliente()
        {
            if (_viewModel.SelectedCliente != null)
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "deberUno.db3");
                await Navigation.PushAsync(new EditarClienteView(_viewModel.SelectedCliente, dbPath));
            }
            else
            {
                await DisplayAlert("Error", "Por favor, seleccione un cliente para editar", "OK");
            }
        }

        private async Task EliminarCliente()
        {
            if (_viewModel.SelectedCliente != null)
            {
                var confirm = await DisplayAlert("Eliminar Cliente", "¿Está seguro que desea eliminar este cliente?", "Sí", "No");
                if (confirm)
                {
                    var resultado = await _clienteRepository.deleteClienteAsync(_viewModel.SelectedCliente.ID); // Pasar el ID del cliente
                    if (resultado)
                    {
                        await _viewModel.CargarClientes(); // Recargar la lista de clientes después de eliminar
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar el cliente", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor, seleccione un cliente para eliminar", "OK");
            }
        }
    }
}


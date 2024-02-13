using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Crud_Sqlite.Model;
using Crud_Sqlite.Service;
using Crud_Sqlite.View;
using Xamarin.Forms;

namespace Crud_Sqlite.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly INavigation _navigation; // Agrega esta línea
        private Cliente _selectedCliente;

        public ObservableCollection<Cliente> Clientes { get; set; }

        public Cliente SelectedCliente
        {
            get { return _selectedCliente; }
            set
            {
                _selectedCliente = value;
                OnPropertyChanged(nameof(SelectedCliente));
            }
        }

        public event EventHandler<string> DisplayAlertRequested;

        public ICommand IrAAgregarClienteCommand { get; }

        public HomeViewModel(IClienteRepository clienteRepository, INavigation navigation) // Modifica este constructor
        {
            _clienteRepository = clienteRepository;
            _navigation = navigation; // Asigna la instancia de INavigation
            Clientes = new ObservableCollection<Cliente>();
            IrAAgregarClienteCommand = new Command(async () => await IrAAgregarCliente());
            Task.Run(async () => await CargarClientes());
        }

        private async Task IrAAgregarCliente()
        {
            await _navigation.PushAsync(new AgregarClienteView(_clienteRepository));
        }

        //public async Task CargarClientes()
        //{
        //    var clientes = await _clienteRepository.getClientesAsync();
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        Clientes.Clear();
        //        foreach (var cliente in clientes)
        //        {
        //            Clientes.Add(cliente);
        //        }
        //    });
        //}

        public async Task CargarClientes()
        {
            try
            {
                var clientes = await _clienteRepository.getClientesAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    Clientes.Clear();
                    foreach (var cliente in clientes)
                    {
                        Clientes.Add(cliente);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar clientes: {ex.Message}");
                DisplayAlertRequested?.Invoke(this, "Ocurrió un error al cargar los clientes");
            }
        }

        private async Task AgregarCliente()
        {
            try
            {
                Cliente nuevoCliente = new Cliente
                {
                    nombre = "Nombre del nuevo cliente",
                    apellido = "Apellido del nuevo cliente",
                    correo = "correo@example.com",
                    psw = "password",
                    estado = true
                };
                bool resultado = await _clienteRepository.addClienteAsync(nuevoCliente);

                if (resultado)
                {
                    DisplayAlertRequested?.Invoke(this, "Nuevo cliente agregado correctamente");
                }
                else
                {
                    DisplayAlertRequested?.Invoke(this, "No se pudo agregar el nuevo cliente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar cliente: {ex.Message}");
                DisplayAlertRequested?.Invoke(this, "Ocurrió un error al agregar el cliente");
            }
        }
    }
}

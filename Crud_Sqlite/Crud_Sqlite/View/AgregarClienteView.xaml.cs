using Crud_Sqlite.Model;
using Crud_Sqlite.Service;
using System;
using Xamarin.Forms;

namespace Crud_Sqlite.View
{
    public partial class AgregarClienteView : ContentPage
    {
        private readonly IClienteRepository _clienteRepository;
        public event EventHandler ClienteAgregado;

        // Define el evento para solicitar la visualización de una alerta
        public event EventHandler<string> DisplayAlertRequested;

        public AgregarClienteView(IClienteRepository clienteRepository)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
        }

        private async void AgregarCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cliente nuevoCliente = new Cliente
                {
                    nombre = NombreEntry.Text,
                    apellido = ApellidoEntry.Text,
                    correo = CorreoEntry.Text,
                    psw = ContraseñaEntry.Text,
                    estado = true
                };

                bool resultado = await _clienteRepository.addClienteAsync(nuevoCliente);

                if (resultado)
                {
                    // Después de agregar el cliente, vuelve a la página anterior (HomeView)
                    await Navigation.PopAsync();

                    // Disparar el evento ClienteAgregado
                    ClienteAgregado?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    // Solicitar visualización de alerta
                    DisplayAlertRequested?.Invoke(this, "No se pudo agregar el nuevo cliente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar cliente: {ex.Message}");
                // Solicitar visualización de alerta
                DisplayAlertRequested?.Invoke(this, "Ocurrió un error al agregar el cliente");
            }
        }

    }
}

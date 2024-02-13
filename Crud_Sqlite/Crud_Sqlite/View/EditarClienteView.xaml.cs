using System;
using Xamarin.Forms;
using Crud_Sqlite.Model;
using Crud_Sqlite.Service;

namespace Crud_Sqlite.View
{
    public partial class EditarClienteView : ContentPage
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly Cliente _cliente;

        public EditarClienteView(Cliente cliente, string dbPath)
        {
            InitializeComponent();

            // Inicializa el repositorio de cliente y almacena el cliente que se está editando
            _clienteRepository = new ClienteService(dbPath);
            _cliente = cliente;

            // Asigna los valores del cliente a los campos de entrada
            NombreEntry.Text = _cliente.nombre;
            ApellidoEntry.Text = _cliente.apellido;
            CorreoEntry.Text = _cliente.correo;
            PswEntry.Text = _cliente.psw;
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Actualiza los datos del cliente con los valores de los campos de entrada
                _cliente.nombre = NombreEntry.Text;
                _cliente.apellido = ApellidoEntry.Text;
                _cliente.correo = CorreoEntry.Text;
                _cliente.psw = PswEntry.Text;

                // Guarda los cambios en la base de datos
                bool resultado = await _clienteRepository.updateClienteAsync(_cliente);

                if (resultado)
                {
                    // Muestra un mensaje de éxito
                    await DisplayAlert("Éxito", "Los datos del cliente se actualizaron correctamente", "OK");
                    // Vuelve a la página anterior
                    await Navigation.PopAsync();
                }
                else
                {
                    // Muestra un mensaje de error si no se pudieron guardar los cambios
                    await DisplayAlert("Error", "No se pudieron guardar los cambios del cliente", "OK");
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre una excepción
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                await DisplayAlert("Error", "Ocurrió un error al actualizar el cliente", "OK");
            }
        }
    }
}

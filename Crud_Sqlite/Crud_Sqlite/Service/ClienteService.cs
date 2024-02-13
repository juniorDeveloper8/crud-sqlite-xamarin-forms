using Crud_Sqlite.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crud_Sqlite.Service
{
    internal class ClienteService : IClienteRepository
    {
        public SQLiteAsyncConnection _database;

        public ClienteService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Cliente>().Wait();
        }
        //insertar
        public async Task<bool> addClienteAsync(Cliente cliente)
        {
            try
            {
                cliente.estado = true;
                int result = await _database.InsertAsync(cliente);
                return result != 0;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción aquí
                Console.WriteLine($"Error al agregar cliente: {ex.Message}");
                return false;
            }
        }
        // eliminar
        public async Task<bool> deleteClienteAsync(int id)
        {
            try
            {
                var cliente = await _database.Table<Cliente>().Where(c => c.ID == id).FirstOrDefaultAsync();
                if (cliente != null)
                {
                    cliente.estado = false;
                    int result = await _database.UpdateAsync(cliente);
                    return result != 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción aquí
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
                return false;
            }
        }
        // listar por id
        public async Task<Cliente> getClienteAsync(int id)
        {
            try
            {
                return await _database.Table<Cliente>().Where(c => c.ID == id && c.estado).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Manejo de la excepción aquí
                Console.WriteLine($"Error al obtener cliente: {ex.Message}");
                return null;
            }
        }
        // listar todo
        public async Task<IEnumerable<Cliente>> getClientesAsync()
        {
            try
            {
                var clientes = await _database.Table<Cliente>().Where(c => c.estado).ToListAsync();
                return clientes;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción aquí
                Console.WriteLine($"Error al obtener clientes: {ex.Message}");
                return null;
            }
        }
        // actualizar
        public async Task<bool> updateClienteAsync(Cliente cliente)
        {
            try
            {
                int result = await _database.UpdateAsync(cliente);
                return result != 0;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción aquí
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                return false;
            }
        }
    }
}

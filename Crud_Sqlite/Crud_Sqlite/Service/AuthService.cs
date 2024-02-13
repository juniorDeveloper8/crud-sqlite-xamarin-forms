using System;
using System.Threading.Tasks;
using SQLite;
using Crud_Sqlite.Model;

namespace Crud_Sqlite.Service
{
    public class AuthService
    {
        private readonly SQLiteAsyncConnection _database;

        public AuthService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public async Task<bool> AutenticarAsync(string correo, string password)
        {
            try
            {
                var usuario = await _database.Table<Usuario>().Where(u => u.Correo == correo && u.Psw == password).FirstOrDefaultAsync();
                return usuario != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al autenticar usuario: {ex.Message}");
                return false;
            }
        }
    }
}

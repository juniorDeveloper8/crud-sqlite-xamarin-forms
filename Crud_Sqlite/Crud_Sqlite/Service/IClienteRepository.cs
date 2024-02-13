using Crud_Sqlite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Sqlite.Service
{
    public interface IClienteRepository
    {
        Task<bool> addClienteAsync(Cliente cliente);
        Task<bool> updateClienteAsync(Cliente cliente);
        Task<bool> deleteClienteAsync(int id);
        Task<Cliente> getClienteAsync(int id);
        Task<IEnumerable<Cliente>> getClientesAsync();
    }
}

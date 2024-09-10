using ClientesTrinity.Repositories;
using static ClientesTrinity.Models.Clientes;

namespace ClientesTrinity.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync() => await _clienteRepository.GetAllAsync();
        public async Task<Cliente> GetByIdAsync(int id) => await _clienteRepository.GetByIdAsync(id);
        public async Task<Cliente> GetByCpfCnpjAsync(string cpfCnpj)
        {
            return await _clienteRepository.GetByCpfCnpjAsync(cpfCnpj);
        }
        public async Task AddAsync(Cliente cliente) => await _clienteRepository.AddAsync(cliente);
        public async Task UpdateAsync(Cliente cliente) => await _clienteRepository.UpdateAsync(cliente);
        public async Task DeleteAsync(int id) => await _clienteRepository.DeleteAsync(id);
    }
}

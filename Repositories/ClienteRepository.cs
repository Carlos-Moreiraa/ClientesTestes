﻿using ClientesTrinity.Data;
using Microsoft.EntityFrameworkCore;
using static ClientesTrinity.Models.Clientes;

namespace ClientesTrinity.Repositories
{
   
        public class ClienteRepository : IClienteRepository
        {
            private readonly ApplicationDbContext _context;

            public ClienteRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Cliente>> GetAllAsync() => await _context.Clientes.ToListAsync();

            public async Task<Cliente> GetByIdAsync(int id) => await _context.Clientes.FindAsync(id);
        public async Task<Cliente> GetByCpfCnpjAsync(string cpfCnpj)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.CpfCnpj == cpfCnpj);
        }


        public async Task AddAsync(Cliente cliente)
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Cliente cliente)
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente != null)
                {
                    _context.Clientes.Remove(cliente);
                    await _context.SaveChangesAsync();
                }
            }
        }    
}

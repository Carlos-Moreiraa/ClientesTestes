using static ClientesTrinity.Models.Clientes;
using Microsoft.EntityFrameworkCore;

namespace ClientesTrinity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
               
            modelBuilder.Entity<Cliente>()
                .Property(c => c.CpfCnpj)
                .HasColumnName("CPF_CNPJ");

            // Definir configurações específicas, se necessário
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CpfCnpj)
                .IsUnique(); 
        }
    }
}

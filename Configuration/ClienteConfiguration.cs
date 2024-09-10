using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ClientesTrinity.Models.Clientes;

namespace ClientesTrinity.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CpfCnpj).IsRequired().HasMaxLength(14);
            builder.HasIndex(c => c.CpfCnpj).IsUnique();  // CPF/CNPJ deve ser único
        }
    }
}

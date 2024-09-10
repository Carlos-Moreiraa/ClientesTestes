using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesTrinity.Models
{
    public class Clientes
    {
        public class Cliente
        {

            public int Id { get; set; }
            [Required]
            [Column("RAZAO_SOCIAL")]
            public string RazaoSocial { get; set; }

            [Required]
            [Column("NOME_FANTASIA")]
            public string NomeFantasia { get; set; }
            [Required]
            [Column("CPF_CNPJ")]
            public string CpfCnpj { get; set; }
        }

    }
}

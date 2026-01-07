using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Bank : Model
    {
        public int Id { get; set; }
        public string Name { get; set; } // Nome do banco
        public string Code { get; set; } // Código identificador do banco (ex: 0033)
    }
}

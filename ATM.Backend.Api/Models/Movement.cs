using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Movement : Model
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        
        // Relacionamento: Uma movimentação pertence a um cartão
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}

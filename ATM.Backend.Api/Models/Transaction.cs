using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Transaction : Model
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Card? DestinyCard { get; set; }
        public Card? SorceCard { get; set; }
        
    }
}

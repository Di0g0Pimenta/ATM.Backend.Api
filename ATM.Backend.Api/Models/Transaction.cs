using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Transaction : Model
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public Card ReceveCard { get; set; }
        public Card OriginCard { get; set; }
        
    }
}

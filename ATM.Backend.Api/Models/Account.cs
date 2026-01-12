using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models

{
    [PrimaryKey(nameof(Id))]
    public class Account : Model
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalBalance { get; set; } = 0;
        public Client Client { get; set; }
        
    }
}

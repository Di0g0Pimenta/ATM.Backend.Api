using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models;

[PrimaryKey(nameof(Id))]
public class CardHistory : Model
{
    public int Id { get; }
    public Card Card { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}
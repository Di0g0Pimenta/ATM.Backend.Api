using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Backend.Api.Models;

public class Services : Model
{
    public int Id { get; set; }
    public int Entity {get; set; }
    public string Name { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public Transaction Transaction { get; set; }
}
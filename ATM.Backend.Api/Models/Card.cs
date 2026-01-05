using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models;


[PrimaryKey(nameof(Id))]
public class Card : Model
{
    public int Id { get; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime ExpiryDate { get; set; } = DateTime.Today.AddYears(4);
    public int Cvv { get; set; } 
}
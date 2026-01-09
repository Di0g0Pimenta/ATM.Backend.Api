namespace ATM.Backend.Api.Models;

public class Services : Model
{
    public int Id { get; set; }
    public int Entity {get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public Transaction Transaction { get; set; }
}
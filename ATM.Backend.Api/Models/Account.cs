using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models

{
    [PrimaryKey(nameof(Id))]
    public class Account : Model
    {
        public int Id { get; set; }
        public double TotalBalance { get; set; } = 0;
        public Client Client { get; set; }
        
    }
}

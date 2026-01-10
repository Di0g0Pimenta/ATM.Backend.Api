using System.Net.Mime;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Client : Model
    {
        public int Id { get; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}

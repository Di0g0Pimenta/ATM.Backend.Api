using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Bank : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

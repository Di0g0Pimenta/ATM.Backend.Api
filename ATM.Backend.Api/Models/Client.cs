using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Client : Model
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        // lista de contas -- Um Ciente tem varias contas
        public List<Account> Accounts { get; set; }
        
        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}

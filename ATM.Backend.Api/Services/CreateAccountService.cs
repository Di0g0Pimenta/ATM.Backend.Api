using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Services;

public class CreateAccountService
{
    public ClientDao _clientDao;
    public AccountDao _accountDao;
    public CardDao _cardDao;
    public BankDao _bankDao;
    
    public CreateAccountService(AppDbContext context)
    {
        _clientDao = new ClientDao(context);
        _accountDao = new AccountDao(context);
        _cardDao = new CardDao(context);
        _bankDao = new BankDao(context);
    }
    
    
    public Client createNewClient(NewClientDto clientDto)
    {
        
        Client client = new Client();
        client.Username = clientDto.Username;
        client.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
        _clientDao.Create(client);
        
        Account account = new Account();
        account.Client = client;
        _accountDao.Create(account);
        
        Bank bank = _bankDao.GetById(clientDto.BankId);
        Card card = new Card(bank, account);
        _cardDao.Create(card);
        return client;
    }
    
}
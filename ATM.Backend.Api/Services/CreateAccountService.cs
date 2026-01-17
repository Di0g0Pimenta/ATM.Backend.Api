using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Services;

public class CreateAccountService
{
    private readonly ClientDao _clientDao;
    private readonly AccountDao _accountDao;
    private readonly CardDao _cardDao;
    private readonly BankDao _bankDao;
    
    public CreateAccountService(ClientDao clientDao, AccountDao accountDao, CardDao cardDao, BankDao bankDao)
    {
        _clientDao = clientDao;
        _accountDao = accountDao;
        _cardDao = cardDao;
        _bankDao = bankDao;
    }
    
    
    public Client createNewClient(NewClientDto clientDto)
    {
        // Verificar se o username já existe
        if (_clientDao.UsernameExists(clientDto.Username))
            throw new InvalidOperationException($"Username '{clientDto.Username}' is already in use.");
        
        Client client = new Client();
        client.Username = clientDto.Username;
        client.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
        _clientDao.Create(client);
        
        Account account = new Account();
        account.Client = client;
        _accountDao.Create(account);
        
        Bank bank = _bankDao.GetById(clientDto.BankId);
        if (bank == null)
            throw new KeyNotFoundException($"Bank with ID {clientDto.BankId} not found.");

        Card card = new Card(bank, account);
        _cardDao.Create(card);
        return client;
    }
    
}
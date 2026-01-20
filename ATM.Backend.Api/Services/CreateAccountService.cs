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
    private readonly AddCardService _addCardService;
    
    public CreateAccountService(AppDbContext context)
    {
        _clientDao = new ClientDao(context);
        _accountDao = new AccountDao(context);
        _cardDao = new CardDao(context);
        _bankDao = new BankDao(context);
        _addCardService = new AddCardService(context);
        
    }
    
    
    public void createNewClient(NewClientDto clientDto)
    {
        // Verificar se o username já existe
        if (_clientDao.UsernameExists(clientDto.Username))
            throw new InvalidOperationException($"Username '{clientDto.Username}' is already in use.");
        
        if (_cardDao.CardNumberExists(clientDto.cardNumber))
            throw new InvalidOperationException($"Card '{clientDto.cardNumber}' is already in use.");

        
        Client client = new Client();
        client.Username = clientDto.Username;
        client.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
        client.ProfileImage = clientDto.ProfileImage; // Opcional
        _clientDao.Create(client);
        
        Account account = new Account();
        account.Client = client;
        _accountDao.Create(account);
        
        
        /*
         acredito que isso nao faz muito sentido,
         pois como é uma comboBox nao ha como o cliente escolher um banco que nao existe
         */
        Bank bank = _bankDao.GetById(clientDto.BankId);
        if (bank == null)
            throw new KeyNotFoundException($"Bank with ID {clientDto.BankId} not found.");
        
        
        _addCardService.AddCard(bank.Id, account.Id, clientDto.cardNumber);
        
    }
    
}
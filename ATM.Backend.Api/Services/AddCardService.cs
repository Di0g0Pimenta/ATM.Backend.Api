using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Services;

public class AddCardService
{
    private readonly CardDao _cardDao;
    private readonly BankDao _bankDao;
    private readonly AccountDao _accountDao;
    
    public AddCardService(AccountDao accountDao, BankDao bankDao, CardDao cardDao)
    {
        _accountDao = accountDao;
        _bankDao = bankDao;
        _cardDao = cardDao;
    }

    public Card AddCard(int bankId, int accountId)
    {
        Bank bank =  _bankDao.GetById(bankId);
        if (bank == null)
            throw new KeyNotFoundException($"Bank with ID {bankId} not found.");

        Account account = _accountDao.GetById(accountId);
        if (account == null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        Card card = new Card(bank, account);
        
        _cardDao.Create(card);
        
        return card;


    }
}
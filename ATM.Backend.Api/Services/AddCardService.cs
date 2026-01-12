using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Services;

public class AddCardService
{
    private readonly CardDao _cardDao;
    private readonly BankDao _bankDao;
    private readonly AccountDao _accountDao;
    
    public AddCardService(AppDbContext context)
    {
        _accountDao = new AccountDao(context);
        _bankDao = new BankDao(context);
        _cardDao = new CardDao(context);
    }

    public Card AddCard(int bankId, int accountId)
    {
        Bank bank =  _bankDao.GetById(bankId);
        Account account = _accountDao.GetById(accountId);

        Card card = new Card(bank, account);
        
        _cardDao.Create(card);
        
        return card;


    }
}
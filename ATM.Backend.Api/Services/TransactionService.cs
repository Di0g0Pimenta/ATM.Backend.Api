using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;

namespace ATM.Backend.Api.Services;

public class TransactionService
{
    private readonly CardDao _cardDao;
    private readonly TransactionDao _transactionDao;
    private readonly AccountDao _accountDao;

    public TransactionService(AppDbContext context)
    {
        _cardDao = new CardDao(context);
        _transactionDao = new TransactionDao(context);
        _accountDao = new AccountDao(context);
    }
    
    public Transaction transactionOperation(TransactionDto transactionDto)
    {
        Transaction transaction = new Transaction();
        transaction.Amount = transactionDto.amount;
        
        if (transactionDto.scrId == -1)
        {
            return deposit(transaction, transactionDto);
        }

        if (transactionDto.dstCardNumber == "")
        {
            return withdraw(transaction, transactionDto);
        }
        
        
        return transfer(transaction, transactionDto);
    }


    private Transaction withdraw(Transaction transaction, TransactionDto transactionDto)
    {
        transaction.Type = "Withdraw";
        transaction.DestinyCard = null;
        transaction.Description = "ATM Withdraw";
        
        Card card = _cardDao.GetById(transactionDto.scrId);
        
        card.Account.TotalBalance -= transactionDto.amount;
        transaction.SorceCard = card;
        card.Withdraw(transaction.Amount);
        
        _accountDao.Update(card.Account);
        _cardDao.Update(card);
        _transactionDao.Create(transaction);
        
        return transaction;
    }

    private Transaction deposit(Transaction transaction, TransactionDto transactionDto)
    {
        transaction.Type = "Deposit";
        transaction.SorceCard = null;
        transaction.Description = "ATM Deposit";
        
        Card card = _cardDao.GetByCardNum(transactionDto.dstCardNumber);
        
        
        card.Account.TotalBalance += transactionDto.amount;
        transaction.DestinyCard = card;
        card.Deposit(transaction.Amount);
        
        _accountDao.Update(card.Account);
        _cardDao.Update(card);
        _transactionDao.Create(transaction);
        
        return transaction;
    }

    private Transaction transfer(Transaction transaction, TransactionDto transactionDto)
    {
        transaction.Type = "Transfer";
        transaction.Description = "Account Transfer";
        
        Card dstCard = _cardDao.GetByCardNum(transactionDto.dstCardNumber);
        Card srcCard = _cardDao.GetById(transactionDto.scrId);

        if (dstCard == null) // If have any card with the desitiny card number
        {
            return null;
        }
        
        transaction.SorceCard = srcCard;
        transaction.DestinyCard = dstCard;
        
        dstCard.Account.TotalBalance += transactionDto.amount;
        srcCard.Account.TotalBalance -= transactionDto.amount;
        dstCard.Deposit(transaction.Amount);
        srcCard.Withdraw(transaction.Amount);
        
        _accountDao.Update(srcCard.Account);
        _accountDao.Update(dstCard.Account);
        _cardDao.Update(dstCard);
        _cardDao.Update(srcCard);
        _transactionDao.Create(transaction);
        
        return transaction;
    }
}
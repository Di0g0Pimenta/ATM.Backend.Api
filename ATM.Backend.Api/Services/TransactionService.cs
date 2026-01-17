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

    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context, CardDao cardDao, TransactionDao transactionDao, AccountDao accountDao)
    {
        _context = context;
        _cardDao = cardDao;
        _transactionDao = transactionDao;
        _accountDao = accountDao;
    }
    
    public Transaction transactionOperation(TransactionDto transactionDto)
    {
        using var transactionScope = _context.Database.BeginTransaction();
        try
        {
            Transaction transaction = new Transaction();
            transaction.Amount = transactionDto.amount;
            
            Transaction result;
            if (transactionDto.scrId == -1)
            {
                result = deposit(transaction, transactionDto);
            }
            else if (string.IsNullOrEmpty(transactionDto.dstCardNumber))
            {
                result = withdraw(transaction, transactionDto);
            }
            else
            {
                result = transfer(transaction, transactionDto);
            }

            transactionScope.Commit();
            return result;
        }
        catch (Exception)
        {
            transactionScope.Rollback();
            throw;
        }
    }


    private Transaction withdraw(Transaction transaction, TransactionDto transactionDto)
    {
        if (transactionDto.amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        transaction.Type = "Withdraw";
        transaction.DestinyCard = null;
        transaction.Description = "ATM Withdraw";
        
        Card card = _cardDao.GetById(transactionDto.scrId);

        if (card == null)
            throw new KeyNotFoundException($"Source card with ID {transactionDto.scrId} not found.");

        if (card.Account == null)
            throw new InvalidOperationException("Source card is not associated with any account.");

        if (card.Account.TotalBalance < transactionDto.amount)
            throw new InvalidOperationException("Insufficient funds.");
        
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
        if (transactionDto.amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        transaction.Type = "Deposit";
        transaction.SorceCard = null;
        transaction.Description = "ATM Deposit";
        
        Card card = _cardDao.GetByCardNum(transactionDto.dstCardNumber);
        
        if (card == null)
            throw new KeyNotFoundException($"Destination card with number {transactionDto.dstCardNumber} not found.");

        if (card.Account == null)
            throw new InvalidOperationException("Destination card is not associated with any account.");
        
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
        if (transactionDto.amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        transaction.Type = "Transfer";
        transaction.Description = "Account Transfer";
        
        Card dstCard = _cardDao.GetByCardNum(transactionDto.dstCardNumber);
        Card srcCard = _cardDao.GetById(transactionDto.scrId);

        if (srcCard == null)
            throw new KeyNotFoundException($"Source card with ID {transactionDto.scrId} not found.");

        if (dstCard == null) 
            throw new KeyNotFoundException($"Destination card with number {transactionDto.dstCardNumber} not found.");

        if (srcCard.Account == null || dstCard.Account == null)
            throw new InvalidOperationException("One or both cards are not associated with an account.");

        if (srcCard.Account.TotalBalance < transactionDto.amount)
            throw new InvalidOperationException("Insufficient funds.");
        
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
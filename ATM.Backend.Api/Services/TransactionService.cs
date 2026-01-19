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

    private static readonly Dictionary<(int Entity, string Reference), string> _servicesCatalog = new()
    {
        { (10001, "0000000001"), "Electricity Bill" },
        { (10002, "0000000002"), "Water Bill" },
        { (10003, "0000000003"), "Internet Bill" },
        { (10004, "0000000004"), "Phone Bill" },
        { (10005, "0000000005"), "Gas Bill" },
        { (10006, "0000000006"), "Insurance Payment" },
        { (10007, "0000000007"), "TV Subscription" },
        { (10008, "0000000008"), "Other Service" }
    };


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

        // Novo: Se Entity e Reference forem fornecidos, é um pagamento de serviço
        if (transactionDto.Entity > 0 && !string.IsNullOrEmpty(transactionDto.Reference))
        {
            if (_servicesCatalog.TryGetValue((transactionDto.Entity, transactionDto.Reference), out var serviceName))
            {
                transaction.Description = serviceName;  // Ex.: "Electricity Bill"
            }
            else
            {
                throw new KeyNotFoundException($"Service with Entity {transactionDto.Entity} and Reference {transactionDto.Reference} not found.");
            }
        }
        else
        {
            transaction.Description = "ATM Withdraw";  // Descrição padrão
        }

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

        // Acredito que isso tambem nao é necessario pois para ele fazer uma transacao ele ja tem que ter o cartao logado
        
        if (srcCard == null)
            throw new KeyNotFoundException($"Source card with ID {transactionDto.scrId} not found.");

        if (dstCard == null) 
            throw new KeyNotFoundException($"Destination card with number {transactionDto.dstCardNumber} not found.");

        // Acredito que isso tambem nao é necessario porque nao tem como ter um cartao sem conta
        
        if (srcCard.Account == null || dstCard.Account == null)
            throw new InvalidOperationException("One or both cards are not associated with an account.");
        
        if (srcCard.Balance < transactionDto.amount)
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
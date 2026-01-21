using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("multibanco/transaction")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    private readonly TransactionDao _transactionDao;
    
    public TransactionController(TransactionService transactionService, ATM.Backend.Api.Repositories.TransactionDao transactionDao)
    {
        _transactionService = transactionService;
        _transactionDao = transactionDao;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionByClient()
    {
        var accountIdClaim = User.FindFirst("AccountId")?.Value;
        if (string.IsNullOrEmpty(accountIdClaim))
            return Unauthorized();

        int accountId = int.Parse(accountIdClaim);
        
        return _transactionDao.ListTransactionByAccountId(accountId);
    }
    
    
    [HttpPut]
    public async Task<ActionResult<Client>> transaction(TransactionDto transactionDto)
    {
        
        Transaction transaction = _transactionService.transactionOperation(transactionDto);

        if (transaction == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
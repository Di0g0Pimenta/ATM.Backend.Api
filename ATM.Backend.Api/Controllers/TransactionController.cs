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
    
    public TransactionController(AppDbContext context)
    {
        this._transactionDao = new TransactionDao(context);
        _transactionService = new TransactionService(context);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransaction()
    {
        return _transactionDao.ListAll();
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
using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("multibanco/transaction")]
[ApiController]
//[Authorize]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    
    public TransactionController(AppDbContext context)
    {
        _transactionService = new TransactionService(context);
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
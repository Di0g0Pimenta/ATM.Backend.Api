using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("api/bank")]
[ApiController]
[Authorize]
public class BankController : ControllerBase
{
    private readonly BankDao bankDao;

    public BankController(AppDbContext context)
    {
        bankDao = new BankDao(context);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bank>>> GetAllBanks()
    {
        return bankDao.ListAll();
    }
}
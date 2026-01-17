using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("api/bank")]
[ApiController]
public class BankController : ControllerBase
{
    private readonly BankDao _bankDao;
    
    public BankController(BankDao bankDao)
    {
        _bankDao = bankDao;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bank>>> GetAllBanks()
    {
        return _bankDao.ListAll();
    }
}
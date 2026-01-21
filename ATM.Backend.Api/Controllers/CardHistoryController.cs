using ATM.Backend.Api.Models;
using ATM.Backend.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("api/cardHistory")]
[ApiController]
[Authorize]
public class CardHistoryController : ControllerBase
{
    private readonly CardHistoryDao _cardHistoryDao;
    
    public CardHistoryController(CardHistoryDao cardHistoryDao)
    {
        _cardHistoryDao = cardHistoryDao;
    }
    
    [HttpGet("list/{cardId}")]
    public async Task<ActionResult<IEnumerable<CardHistory>>> GetAllCardHistory(int cardId)
    {
        return _cardHistoryDao.ListAllByCardId(cardId);
    }
}
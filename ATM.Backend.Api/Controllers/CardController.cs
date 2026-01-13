using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("multibanco/card")]
[ApiController]
//[Authorize]
public class CardController : ControllerBase
{
  
  private readonly AddCardService addCardService;
  private readonly CardDao cardDao;
  
  public CardController(AppDbContext context)
  {
    cardDao = new CardDao(context);
    addCardService = new AddCardService(context);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Card>> getCard(int id)
  {
    Card card = cardDao.GetById(id);
    
    if (card == null)
    {
      return NotFound();
    }
    
    return card;
  }

  [HttpGet("/multibanco/card/listAccountCards/{accountId}")]
  public async Task<ActionResult<IEnumerable<Card>>> getAllCardsByAccountId(int accountId)
  {
    return cardDao.ListAll(accountId);
  }
  
  
  [HttpPost("add/{accountId}/{bankId}")]
  public async Task<ActionResult<Card>> addCard(int bankId, int accountId)
  {
    
    Card newCard = addCardService.AddCard(bankId, accountId);
    
    return CreatedAtAction(nameof(getCard), new { id = newCard.Id }, newCard);
  }
}
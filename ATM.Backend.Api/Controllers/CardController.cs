using ATM.Backend.Api.Dto;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Backend.Api.Controllers.Rest;

[Route("multibanco/card")]
[ApiController]
[Authorize]
public class CardController : ControllerBase
{
  
  private readonly AddCardService _addCardService;
  private readonly CardDao _cardDao;
  
  public CardController(AddCardService addCardService, CardDao cardDao)
  {
    _addCardService = addCardService;
    _cardDao = cardDao;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Card>> getCard(int id)
  {
    Card card = _cardDao.GetById(id);
    
    if (card == null)
    {
      return NotFound();
    }
    
    return card;
  }

  [HttpGet("/listAccountCards/{accountId}")]
  public async Task<ActionResult<IEnumerable<Card>>> getAllCardsByAccountId(int accountId)
  {
    return _cardDao.ListAll(accountId);
  }
  
  
  [HttpPost("add/")]
  public async Task<ActionResult<Card>> addCard(NewCardDto newCardDto)
  {
    _addCardService.AddCard(newCardDto.bankId, newCardDto.accountId, newCardDto.cardNumber);
    
    return Created();
  }
}

using Microsoft.AspNetCore.Mvc;
using OICT_Test.Helpers;
using OICT_Test.Models;
using OICT_Test.Services;

namespace OICT_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuth]
    public class CardController : ControllerBase
    {
        private readonly ICardService _myCardService;
        public CardController(ICardService myCardService)
        {
            _myCardService = myCardService;
        }

        /// <summary>
        /// Zkontrolujte stav a platnost uživatelské karty
        /// </summary>
        /// <param name="cardNumber">Format - string. Číslo uživatelské karty</param>
        /// <returns>Vrátí stav a platnost karty jako text</returns>
        [HttpGet("checkcardstatusandvalidity/{cardNumber}")]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<string>> CheckCardStatusAndValidity(string cardNumber)
        {
            var statusTask = _myCardService.CheckCardStatus(cardNumber);
            var validityTask = _myCardService.CheckCardValidity(cardNumber);

            await Task.WhenAll(statusTask, validityTask);

            CardResponse status = await statusTask;
            CardResponse validity = await validityTask;

            if (!status.Success || !validity.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Něco se pokazilo");
            }

            string combinedResponse = $"{status.Data} - {validity.Data}";
            return Ok(combinedResponse);
        }

    }
}

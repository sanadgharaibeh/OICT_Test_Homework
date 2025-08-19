using OICT_Test.Models;

namespace OICT_Test.Services
{
    public interface ICardService
    {
        Task<CardResponse> CheckCardStatus(string cardNumber);
        Task<CardResponse> CheckCardValidity(string cardNumber);
    }
}

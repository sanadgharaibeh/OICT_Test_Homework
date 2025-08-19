using Microsoft.Extensions.Options;
using OICT_Test.Helpers;
using OICT_Test.Models;
using System.Globalization;

namespace OICT_Test.Services
{
    public class CardService : ICardService
    {
        private readonly AppSettingsWrapper _settings;
        private readonly HttpClient _httpClient;

        public CardService(IOptions<AppSettingsWrapper> options, HttpClient httpClient)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<CardResponse> CheckCardStatus(string cardNumber) =>
            GetCardData<CardStatus>(
                string.Format(_settings.StatusUrl, cardNumber),
                status => status.Description,
                "Nelze přečíst stav karty"
            );

        public Task<CardResponse> CheckCardValidity(string cardNumber) =>
            GetCardData<CardValidity>(
                string.Format(_settings.ValidityUrl, cardNumber),
                validity => validity.ValidityEnd.ToString("dd.M.yyyy", CultureInfo.InvariantCulture),
                "Nelze přečíst platnost karty"
            );

        private async Task<CardResponse> GetCardData<T>(
            string url,
            Func<T, string> dataSelector,
            string errorMessage) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new CardResponse
                    {
                        Success = false,
                        ErrorMessage = $"Error: {response.StatusCode}"
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<T>();

                if (result != null)
                {
                    return new CardResponse
                    {
                        Success = true,
                        Data = dataSelector(result)
                    };
                }

                return new CardResponse
                {
                    Success = false,
                    ErrorMessage = errorMessage
                };
            }
            catch (Exception)
            {
                //V případě potřeby můžeme zaznamenat exception do loggeru.
                return new CardResponse
                {
                    Success = false,
                    ErrorMessage = "Něco se pokazilo"
                };
            }
        }
    }
}

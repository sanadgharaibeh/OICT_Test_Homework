namespace OCIT_UnitTests
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using OICT_Test.Controllers;
    using OICT_Test.Models;
    using OICT_Test.Services;
    using Xunit;

    public class CardControllerTests
    {
        private readonly Mock<ICardService> _mockCardService;
        private readonly CardController _controller;

        public CardControllerTests()
        {
            _mockCardService = new Mock<ICardService>();
            _controller = new CardController(_mockCardService.Object);
        }

        [Fact]
        public async Task CheckCardStatusAndValidity_ReturnsOk_WithCombinedResponse()
        {
            // Arrange
            string cardNumber = "12345";
            _mockCardService.Setup(s => s.CheckCardStatus(cardNumber))
                .ReturnsAsync(new CardResponse { Success = true, Data = "Active" });

            _mockCardService.Setup(s => s.CheckCardValidity(cardNumber))
                .ReturnsAsync(new CardResponse { Success = true, Data = "Valid" });

            // Act
            var result = await _controller.CheckCardStatusAndValidity(cardNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Active - Valid", okResult.Value);
        }

        [Fact]
        public async Task CheckCardStatusAndValidity_Returns500_WhenStatusFails()
        {
            // Arrange
            string cardNumber = "12345";
            _mockCardService.Setup(s => s.CheckCardStatus(cardNumber))
                .ReturnsAsync(new CardResponse { Success = false, Data = "Error" });

            _mockCardService.Setup(s => s.CheckCardValidity(cardNumber))
                .ReturnsAsync(new CardResponse { Success = true, Data = "Valid" });

            // Act
            var result = await _controller.CheckCardStatusAndValidity(cardNumber);

            // Assert
            var statusResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusResult.StatusCode);
        }

        [Fact]
        public async Task CheckCardStatusAndValidity_Returns500_WhenValidityFails()
        {
            // Arrange
            string cardNumber = "12345";
            _mockCardService.Setup(s => s.CheckCardStatus(cardNumber))
                .ReturnsAsync(new CardResponse { Success = true, Data = "Active" });

            _mockCardService.Setup(s => s.CheckCardValidity(cardNumber))
                .ReturnsAsync(new CardResponse { Success = false, Data = "Expired" });

            // Act
            var result = await _controller.CheckCardStatusAndValidity(cardNumber);

            // Assert
            var statusResult = Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusResult.StatusCode);
        }
    }

}
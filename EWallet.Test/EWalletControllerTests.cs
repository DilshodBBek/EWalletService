using EWalletService.Application.UseCases.EWalletCQRS;
using EWalletService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EWallet.Test
{
    [TestFixture]
    public class EWalletControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private EWalletController _controller;


        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EWalletController(_mediatorMock.Object);
        }

        [Test]
        public async Task CreateWallet_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var wallet = new CreateWalletCommand()
            {
                IsIdentified = true,
                Amount = 10
            };
            var expectedResult = new OkObjectResult("Some result");

            _mediatorMock.Setup(m => m.Send(wallet, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateWallet(wallet);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task Balance_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var balance = new BalanceCommand()
            {
                WalletId = 123
            };
            var expectedResult = new OkObjectResult("Some result");

            _mediatorMock.Setup(m => m.Send(balance, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Balance(balance);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task Replenish_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var replenish = new ReplenishCommand()
            {
                PaymentAmount = 10,
                SenderWalletId = 123,
                ReceiverWalletId = 456
            };
            var expectedResult = new OkObjectResult("Some result");

            _mediatorMock.Setup(m => m.Send(replenish, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Replenish(replenish);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task Statistics_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var statistics = new StatisticsCommand()
            {
                StartDate = new System.DateTime(2023, 3, 1),
                EndDate = new System.DateTime(2023, 3, 14)
            };
            var expectedResult = new OkObjectResult("Some result");

            _mediatorMock.Setup(m => m.Send(statistics, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Statistics(statistics);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(expectedResult, result);
        }
    }
}

using WorldDoomLeague.Application.Common.Behaviours;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Application.Players.Commands.CreatePlayer;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreatePlayerCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreatePlayerCommand>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");

            var requestLogger = new LoggingBehaviour<CreatePlayerCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreatePlayerCommand { PlayerAlias = "Kilgore", PlayerName = "KBlair" }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreatePlayerCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreatePlayerCommand { PlayerAlias = "Kilgore", PlayerName = "KBlair" }, new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}

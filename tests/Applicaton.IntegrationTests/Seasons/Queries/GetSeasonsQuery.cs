using WorldDoomLeague.Application.Seasons.Queries.GetSeasons;
using WorldDoomLeague.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;


namespace WorldDoomLeague.Application.IntegrationTests.Seasons.Queries
{
    using static Testing;

    public class GetSeasonsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnSeasons()
        {
            var query = new GetSeasonsQuery();

            var result = await SendAsync(query);

            result.SeasonList.Should().NotBeEmpty();
        }
    }
}

using FluentAssertions;
using System.Net;

namespace apsys.heartbeat.webapi.testing.Controllers
{
    internal class HomeControllerTests : WebApiControllerBaseTests
    {
        [Test]
        public async Task Greet_ValidRequest_ReturnOK()
        {
            // Arrange
            var client = this.CreateClient("adimbptm");
            // Act
            var response = await client.GetAsync("home/greeting");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            string contentResult = await response.Content.ReadAsStringAsync();
            contentResult.Should().Be("Hello world from web api");
        }
    }
}

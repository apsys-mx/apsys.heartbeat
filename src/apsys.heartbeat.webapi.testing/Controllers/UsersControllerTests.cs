using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace apsys.heartbeat.webapi.testing.Controllers
{
    internal class UsersControllerTests : WebApiControllerBaseTests
    {
        [Test]
        public void GetAll_RequestedByAdministrator_ReturnAllUsers()
        {
            // Arrange
            this.LoadScenario("InitializeApplication");
            string userName = "adimbptm";
            var client = this.CreateClient(userName);
            // Act
            var response = client.GetAsync("users").Result;
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = response.Content.ReadAsStringAsync().Result;
            var allUsers = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(content);
            allUsers.Should().HaveCount(1);
        }

        [Test]
        public void GetCurrent_ExistingUser_ReturnUserProfile()
        {
            // Arrange
            this.LoadScenario("InitializeApplication");
            string userName = "adimbptm";
            var client = this.CreateClient(userName);
            // Act
            var response = client.GetAsync("users/current").Result;
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = response.Content.ReadAsStringAsync().Result;
            var currentUser = JsonConvert.DeserializeObject<object>(content);
            currentUser.Should().NotBeNull();
        }

        [Test]
        public void GetCurrent_NonExistingUser_ReturnUserProfile()
        {
            // Arrange
            this.LoadScenario("InitializeApplication");
            string userName = "adimptm";
            var client = this.CreateClient(userName);
            // Act
            var response = client.GetAsync("users/current").Result;
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}

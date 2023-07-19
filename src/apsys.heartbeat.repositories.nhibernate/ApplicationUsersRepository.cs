using apsys.heartbeat.authorization;
using apsys.heartbeat.authorization.daos;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using NHibernate;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace apsys.heartbeat.repositories.nhibernate
{
    internal class ApplicationUsersRepository : Repository<ApplicationUser>, IApplicationUsersRepository
    {
        private readonly IConfiguration _configuration;

        public ApplicationUsersRepository(ISession session, IConfiguration configuration)
            : base(session)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get an application user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ApplicationUser GetByUserName(string userName) => this.Get(x => x.UserName == userName).FirstOrDefault();

        /// <summary>
        /// Get from identity server
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IdentityUserDao GetFromIdentityServer(string userName)
        {
            string[] userNameArray = new string[] { userName };
            StringContent content = new StringContent(JsonSerializer.Serialize(userNameArray), Encoding.UTF8, "application/json");
            var response = this.PostAsync("users", content);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var allUsersInIdentityServer = JsonSerializer.Deserialize<IEnumerable<IdentityUserDao>>(responseContent);
                return allUsersInIdentityServer.FirstOrDefault();
            }
            else
                return null;
        }

        private HttpResponseMessage PostAsync(string relativeUrl, HttpContent content)
        {
            string identityServerUrl = this._configuration.GetSection("IdentityServerConfiguration:Address").Value;
            if (string.IsNullOrEmpty(identityServerUrl))
                throw new ConfigurationErrorsException("No [IdentityServerConfiguration:Address] key found in the configuration file");

            var tokenResponse = GetTokenResponse(identityServerUrl);
            if (tokenResponse.HttpStatusCode != HttpStatusCode.OK)
                throw new ConfigurationErrorsException(tokenResponse.Error);

            using var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            string fullURL = $"{identityServerUrl}{relativeUrl}";
            return client.PostAsync(fullURL, content).Result;
        }
        private TokenResponse GetTokenResponse(string identityServerUrl)
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(identityServerUrl).Result;
            if (disco.IsError)
                throw new ConfigurationErrorsException(disco.Error);

            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "alpunto.outsourcing.webapi",
                ClientSecret = "3XSHkG4AhpxV",
                Scope = "identityServer.api"
            }).Result;

            return tokenResponse;
        }

    }
}

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Azure.Data.AppConfiguration;

namespace MsalIdentity
{
    class Program
    {
        private const string _clientId = "a0fa4f65-4242-406d-b416-65dce1c24f47";
        private const string _tenantId = "c39f8326-8e0b-43fd-8f8d-887f48234991";

        public static async Task Main(string[] args)
        {
            var clients = new ConfigurationClient("Endpoint=https://appvonfig.azconfig.io;Id=ODk8-l9-s0:vo1HWikX5N72EzhSLphV;Secret=5ULlQzXJclJnmDWQi94HSjpMm+mJHmX5by8Vma1PFbk=");
            var v = clients.GetConfigurationSetting("testvl");
            var v2 = clients.GetConfigurationSetting("testvl", "2");



            string[] scopes = { "user.read", "Application.Read.All" };

            var app = PublicClientApplicationBuilder
                        .Create(_clientId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                        .WithRedirectUri("http://localhost")
                        .Build();

            //var appPr = ConfidentialClientApplicationBuilder
            //     .Create(_clientId)
            //     .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
            //     .WithRedirectUri("http://localhost")
            //     .Build();

            //appPr.AcquireTokenByAuthorizationCode()

           // var provider = new InteractiveAuthenticationProvider(app, scopes);

            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);

            var client = new GraphServiceClient(provider);
            User me = await client.Me.Request().GetAsync();
            var apps = await client.Applications.Request().GetAsync();





            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            string url = "https://graph.microsoft.com/v1.0/me";
            string response = await httpClient.GetStringAsync(url);



        }
    }
}

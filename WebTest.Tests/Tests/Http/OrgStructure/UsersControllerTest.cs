using Newtonsoft.Json.Linq;
using System.Net;
using WebTest.Models.OrgStructure;

namespace WebTest.Tests.Tests.Http.OrgStructure
{
    public class UsersControllerTest(TestWebApplicationFactory<Program> factory) : BaseTest(factory)
    {
        const string routeUrl = "/api/users";

        [Fact]
        public async Task GetUnauthrized()
        {
            var client = CreateClient();
            var response = await client.GetAsync(routeUrl);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetList()
        {
            var user = new User()
            {
                Login = "user",
                Password = "password",
            };
            var token = AuthorizedAs(user, db);
            var client = CreateClient(token);
            var response = await client.GetAsync(routeUrl);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            dynamic json = JObject.Parse(content);
            var data = (JArray)json.items;
            List<string> logins = [];
            foreach (dynamic item in (JArray)json.items)
            {
                logins.Add((string)item.login);
            }

            Assert.Empty(logins.Except(new[] { "test", "user" }));
        }
    }
}

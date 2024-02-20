using System.Net;
using System.Text.Json.Nodes;
using WebTest.Models.OrgStructure;
using WebTest.Utils;

namespace WebTest.Tests.Http.OrgStructure
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
            await db.Transaction(async () =>
            {
                ReinitializeDatabase();

                var user = new User()
                {
                    Login = "user",
                    Password = "password",
                };
                var token = AuthorizedAs(user);
                var client = CreateClient(token);
                var response = await client.GetAsync(routeUrl);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var content = await response.Content.ReadAsStringAsync();
                var users = JsonValue.Parse(content)?["items"] as JsonArray;
                var names = users?.Select(n => n?["UserName"]).ToArray();
            });
        }
    }
}

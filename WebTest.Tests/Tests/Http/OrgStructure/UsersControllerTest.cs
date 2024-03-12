using Newtonsoft.Json.Linq;
using System.Net;
using WebTest.Models.OrgStructure;
using WebTest.Dto.OrgStructure.Request;
using System.Net.Http.Json;

namespace WebTest.Tests.Tests.Http.OrgStructure
{
    public class UsersControllerTest(TestWebApplicationFactory<Program> factory) : TestBase(factory)
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
            var client = CreateClient(user);
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

        [Fact]
        public async Task CreateUser()
        {
            var user = new User()
            {
                Login = "user",
                Password = "password",
            };

            var dto = new CreateDto()
            {
                Login = "user2",
                Password = "password2",
            };
            var json = JsonContent.Create(dto);
            var client = CreateClient(user);
            var response = await client.PostAsync(routeUrl, json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var check = db.Users.FirstOrDefault(t => t.Login == dto.Login);

            Assert.NotNull(check);
        }

        [Fact]
        public async Task CreateUserWithNotUiniqueLogin()
        {
            var user = new User()
            {
                Login = "user",
                Password = "password",
            };

            var dto = new CreateDto()
            {
                Login = "user",
                Password = "password2",
            };
            var json = JsonContent.Create(dto);
            var client = CreateClient(user);
            var response = await client.PostAsync(routeUrl, json);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

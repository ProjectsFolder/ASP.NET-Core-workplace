using Api.Responses;
using Application.Extensions;
using System.Text;
using System.Text.Json;
using Api.Requests.User;
using System.Net;

namespace Tests.Api.User;

[TestClass]
public class CreateTest : BaseTest
{
    [TestMethod]
    [DataRow("test", "test@mail.com", HttpStatusCode.OK)]
    [DataRow("test", null, HttpStatusCode.OK)]
    [DataRow("admin", "test@mail.com", HttpStatusCode.InternalServerError)]
    public async Task Create(string testUsername, string? testEmail, HttpStatusCode statusCode)
    {
        var clientUser = Database.Users.Where(u => u.Login.Equals("admin")).First();
        var client = await CreateClient(clientUser);

        var request = new CreateUserRequest()
        {
            Login = testUsername,
            Password = "test",
            Email = testEmail,
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/v1/user", jsonContent);

        Assert.AreEqual(statusCode, response.StatusCode);

        if (statusCode == HttpStatusCode.OK)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseDto = stringResponse.FromJson<SuccessResponse<int>>();

            Assert.IsNotNull(responseDto);

            var userId = responseDto.Item;
            var user = Database.Users.Where(u => u.Id == userId).FirstOrDefault();

            Assert.IsNotNull(user);
        }
    }
}

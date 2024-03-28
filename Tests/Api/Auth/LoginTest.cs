using Application.Extensions;
using Api.Requests.Auth;
using Api.Responses;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Tests.Api.Auth;

[TestClass]
public class LoginTest : BaseTest
{
    [TestMethod]
    [DataRow("admin", "password", HttpStatusCode.OK)]
    [DataRow("admin", "badpassword", HttpStatusCode.Unauthorized)]
    [DataRow("user", "badpassword", HttpStatusCode.NotFound)]
    public async Task Login(string testUsername, string testPassword, HttpStatusCode statusCode)
    {
        var request = new AuthRequest()
        {
            Login = testUsername,
            Password = testPassword
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await NewClient.PostAsync("api/v1/auth/login", jsonContent);

        Assert.AreEqual(statusCode, response.StatusCode);

        if (statusCode == HttpStatusCode.OK)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var tokenString = stringResponse.FromJson<SuccessResponse<string>>()?.Item;
            var token = Database.Tokens.Where(t => t.Value.Equals(tokenString)).FirstOrDefault();

            Assert.IsNotNull(token);
        }
    }
}

using System.Net;

namespace Tests.Api.Auth;

[TestClass]
public class LogoutTest : BaseTest
{
    [TestMethod]
    public async Task Logout()
    {
        var user = Database.Users.Where(u => u.Login.Equals("admin")).First();
        var client = await CreateClient(user);
        var response = await client.PostAsync("api/v1/auth/logout", null);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsFalse(Database.Tokens.Any(t => t.UserId == user.Id));
    }
}

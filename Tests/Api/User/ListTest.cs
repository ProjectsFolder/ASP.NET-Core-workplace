using Api.Responses;
using Api.Responses.User;
using Application.Extensions;

namespace Tests.Api.User;

[TestClass]
public class ListTest : BaseTest
{
    [TestMethod]
    public async Task List()
    {
        var user = Database.Users.Where(u => u.Login.Equals("admin")).First();
        var client = await CreateClient(user);
        var response = await client.GetAsync("api/v1/user");
        var stringResponse = await response.Content.ReadAsStringAsync();
        var responseDto = stringResponse.FromJson<SuccessResponse<IEnumerable<UserResponse>>>();

        Assert.IsNotNull(responseDto);
        Assert.IsNotNull(responseDto.Items);
        Assert.IsTrue(responseDto.Items.Count() == 1);

        var userDto = responseDto.Items.ElementAt(0);

        Assert.AreEqual(userDto.Login, "admin");
    }
}

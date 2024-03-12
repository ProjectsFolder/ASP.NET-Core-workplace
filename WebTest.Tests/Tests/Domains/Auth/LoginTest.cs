using WebTest.Domains.Auth.Handlers;
using WebTest.Dto.Auth.Command;
using WebTest.Exeptions.Concrete;
using WebTest.Models.Auth;

namespace WebTest.Tests.Tests.Domains.Auth
{
    public class LoginTest(TestWebApplicationFactory<Program> factory) : TestBase(factory)
    {
        [Fact]
        public void SuccessLogin()
        {
            var handler = GetService<Login>();
            var dto = new AuthCommand("test", "test");
            var response = handler.Handle(dto);
            if (response.Item != null)
            {
                var token = db.Tokens.FirstOrDefault(t => t.Value == response.Item.Token);
                Assert.NotNull(token);
            }
        }

        [Fact]
        public void FailureLogin()
        {
            var handler = GetService<Login>();
            var dto = new AuthCommand("test", "password");
            Assert.Throws<ApiException>(() => handler.Handle(dto));
        }
    }
}

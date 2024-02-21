using WebTest.Domains.Auth.Handlers;
using WebTest.Dto.Auth.Request;
using WebTest.Exeptions.Concrete;

namespace WebTest.Tests.Domains.Auth
{
    public class LoginTest(TestWebApplicationFactory<Program> factory) : BaseTest(factory)
    {
        [Fact]
        public void SuccessLogin()
        {
            var handler = GetService<Login>();
            var dto = new AuthDto("test", "test");
            var response = handler?.Handle(dto);

            var token = db.Tokens.FirstOrDefault(t => t.Value == response.Token);

            Assert.NotNull(token);
        }

        [Fact]
        public void FailureLogin()
        {
            var handler = GetService<Login>();
            var dto = new AuthDto("test", "password");
            Assert.Throws<ApiException>(() => handler?.Handle(dto));
        }
    }
}

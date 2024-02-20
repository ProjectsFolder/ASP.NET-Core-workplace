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
            db.Transaction(() =>
            {
                ReinitializeDatabase();
                var handler = GetService<Login>();
                var dto = new AuthDto("test", "test");
                var response = handler?.Handle(dto);

                var token = db.Tokens.FirstOrDefault(t => t.Value == response.Token);
                var temp = db.Users.ToArray();

                Assert.NotNull(token);
            });
        }

        [Fact]
        public void FailureLogin()
        {
            db.Transaction(() =>
            {
                ReinitializeDatabase();
                var handler = GetService<Login>();
                var dto = new AuthDto("test", "password");
                Assert.Throws<ApiException>(() => handler?.Handle(dto));
            });
        }
    }
}

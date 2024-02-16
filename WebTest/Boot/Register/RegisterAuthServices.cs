using WebTest.Domains.Auth.Handlers;

namespace WebTest.Boot.Register
{
    public static class RegisterAuthServices
    {
        public static void AddAuthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Logout>();
        }
    }
}

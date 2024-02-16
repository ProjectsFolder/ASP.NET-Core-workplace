using WebTest.Domains.User.Handlers;

namespace WebTest.Boot.Register
{
    public static class RegisterUserServices
    {
        public static void AddUserServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ListUsers>();
            builder.Services.AddTransient<CreateUser>();
            builder.Services.AddTransient<UpdateUser>();
            builder.Services.AddTransient<DeleteUser>();
        }
    }
}


using System.Web;

namespace ConsoleClient.Commands
{
    internal class MigrateCommand : BaseCommand
    {
        public override async Task<Result> Execute(Dictionary<string, string>? args = null)
        {
            var builder = new UriBuilder("http://webtest/api/service/migrate")
            {
                Port = 8082
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (args != null)
            {
                foreach (var data in args)
                {
                    query[data.Key] = data.Value;
                }
            }
            builder.Query = query.ToString();

            var client = new HttpClient();
            var response = await client.PostAsync(builder.ToString(), null);
            var result = await response.Content.ReadAsStringAsync();
            Print(result);

            return Result.Success;
        }

        public override string GetCommand()
        {
            return "migrate";
        }
    }
}

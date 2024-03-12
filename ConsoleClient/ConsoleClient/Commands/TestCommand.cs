
namespace ConsoleClient.Commands
{
    internal class TestCommand : CommandBase
    {
        public override async Task<Result> Execute(Dictionary<string, string>? args = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", "secret");
            var response = await client.GetAsync("http://webtest:8082/api/export");
            var result = await response.Content.ReadAsStringAsync();
            Print(result);

            return Result.Success;
        }

        public override string GetCommand()
        {
            return "web:test";
        }
    }
}

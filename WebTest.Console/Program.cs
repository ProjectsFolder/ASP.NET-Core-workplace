// See https://aka.ms/new-console-template for more information
Thread.Sleep(5000);
var client = new HttpClient();
client.DefaultRequestHeaders.Add("X-Auth-Token", "secret");
client.GetAsync("http://webtest:8082/api/export").Wait();

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace NeuralNetAsp.ApiTests
{
  [TestClass]
  public class NeuralNetApiTests
  {
    string url = "http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com/api/values/";

    static readonly HttpClient client = new HttpClient();

    [TestMethod]
    public async Task TestPlaceholderApiRoundtrip()
    {

      using HttpResponseMessage response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();
      Console.WriteLine(responseBody);

    }
  }

  public class PlaceholderResponse
  {
    [JsonProperty("values")]
    public String[] Values { get; set; }
  }
}
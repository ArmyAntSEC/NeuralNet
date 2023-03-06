using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NeuralNetAsp.HeartbeatApiTests
{
  [TestClass]
  public class HeartbeatApiTest
  {

    string baseUrl = "http://localhost:/api";

    static readonly HttpClient httpClient = new HttpClient();

    [TestMethod]
    public void TestHeartbeatApi()
    {
      var url = baseUrl + "/heartbeat";
      using (HttpResponseMessage message = httpClient.GetAsync(url).Result)
      {
        Assert.AreEqual(true, message.IsSuccessStatusCode);

        var deserializedObject = JsonSerializer.Deserialize<HeartbeatResponse>(message.Content.ReadAsStringAsync().Result);
        Assert.AreEqual("Alive!", deserializedObject!.Status);
      }
    }
  }
  public class HeartbeatResponse
  {
    [JsonPropertyName("status")]
    public String Status { get; set; }

    public HeartbeatResponse(String status)
    {
      Status = status;
    }
  }
}
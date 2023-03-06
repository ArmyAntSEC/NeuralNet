using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

using NeuralNetAsp.Controllers;

namespace NeuralNetAsp.NeuralNetApiTests
{
  [TestClass]
  public class NeuralNetApiTest
  {

    string baseUrl = "http://localhost:5000/api";

    static readonly HttpClient httpClient = new HttpClient();

    [TestMethod]
    public void TestTrainNetwork()
    {
      var url = baseUrl + "/neural";

      var request = new TrainingData();
      request.Input = new double[] { -0.33, -0.33, -0.33, -0.33, -0.33, 0.69, 0.94, 0.5, 0.75, 0.67, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1 };
      request.Output = new double[] { 1, 1, 0, 1, 0 };

      using (var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json"))
      {
        using (HttpResponseMessage message = httpClient.PostAsync(url, content).Result)
        {
          Assert.AreEqual(true, message.IsSuccessStatusCode);
          var body = message.Content.ReadAsStringAsync().Result;
          Console.WriteLine("*** Response body: " + body);
          var deserializedObject = JsonSerializer.Deserialize<ResponseData>(body);
          Assert.AreEqual(0.20501795434531944, deserializedObject.LayerOneWeights[0]);
        }
      }
    }
  }
}
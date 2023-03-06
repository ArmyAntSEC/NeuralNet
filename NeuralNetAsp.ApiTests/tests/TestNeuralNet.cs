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

          Assert.AreEqual("{\"layer_one_weights\":[0.20501795434531944,4.851008781645799,-5.094595716697625,-3.173856984876353,-0.3819715353811027,5.729700533135147,-7.097541432816082,-3.6949973939179026,-0.11692806431143415,-3.895180491131896,2.0547548660462955,1.6979619539598476],\"layer_two_weights\":[5.44573619851985,5.522509842250539,-4.9276948411470585]}", body);
        }
      }
    }

    [TestMethod]
    public void TestDoPrognosis()
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

          Assert.AreEqual("{\"layer_one_weights\":[0.20501795434531944,4.851008781645799,-5.094595716697625,-3.173856984876353,-0.3819715353811027,5.729700533135147,-7.097541432816082,-3.6949973939179026,-0.11692806431143415,-3.895180491131896,2.0547548660462955,1.6979619539598476],\"layer_two_weights\":[5.44573619851985,5.522509842250539,-4.9276948411470585]}", body);
        }
      }
    }
  }
}
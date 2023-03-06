using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;

using NeuralNetAsp.Controllers;

namespace NeuralNetAsp.NeuralNetApiTests
{
  [TestClass]
  public class NeuralNetApiTest
  {

    string baseUrl = "http://localhost:80/api";

    static readonly HttpClient httpClient = new HttpClient();

    [TestMethod]
    public void TestTrainNetwork()
    {
      var url = baseUrl + "/trainer";

      var request = new TrainingData();
      request.Input = new double[] { -0.33, -0.33, -0.33, -0.33, -0.33, 0.69, 0.94, 0.5, 0.75, 0.67, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1 };
      request.Output = new double[] { 1, 1, 0, 1, 0 };

      using (var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json"))
      {
        using (HttpResponseMessage message = httpClient.PostAsync(url, content).Result)
        {
          Assert.AreEqual(true, message.IsSuccessStatusCode);
          var body = message.Content.ReadAsStringAsync().Result;
          var response = JsonNode.Parse(body);

          //Check two example values. Since this is a test, we don't ned to worry about dereferencing nulls
          Assert.AreEqual(0.2050179, (double)response["layerOneWeights"][0], 1e-5);
          Assert.AreEqual(5.445736198, (double)response["layerTwoWeights"][0], 1e-5);
        }
      }
    }

    [TestMethod]
    public void TestPredict()
    {
      var url = baseUrl + "/predictor";

      var request = new PredictData();
      request.LayerOneWeights = new double[] { 0.20501795434531928, 4.851008781645798, -5.094595716697624, -3.173856984876353, -0.3819715353811023, 5.729700533135146, -7.097541432816082, -3.6949973939179026, -0.1169280643114341, -3.895180491131896, 2.054754866046295, 1.697961953959847 };
      request.LayerTwoWeights = new double[] { 5.44573619851985, 5.522509842250541, -4.927694841147059 };
      request.Input = new double[] { -0.33, 0.69, 0, 1 };

      using (var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json"))
      {
        using (HttpResponseMessage message = httpClient.PostAsync(url, content).Result)
        {
          Assert.AreEqual(true, message.IsSuccessStatusCode);
          var body = message.Content.ReadAsStringAsync().Result;

          Assert.AreEqual(0.991667256, Convert.ToDouble(body, System.Globalization.CultureInfo.InvariantCulture), 1e-5);
        }
      }
    }

  }
}
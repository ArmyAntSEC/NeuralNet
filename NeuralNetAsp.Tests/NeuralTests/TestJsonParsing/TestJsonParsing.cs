using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json.Nodes;

namespace NeuralNetAsp.UnitTests.JsonParsing;

[TestClass]
public class TestJsonParse
{
  [TestMethod]
  public void TestParseJson()
  {
    var jsonString = "{\"hello\":\"world\"}";
    JsonNode? helloWorld = JsonNode.Parse(jsonString);
    Assert.AreEqual("world", (String)helloWorld!["hello"]);
  }

  [TestMethod]
  public void TestParseOtherJson()
  {
    var jsonString = "{\"layerOneWeights\":[0.20501795434531944,4.851008781645799,-5.094595716697625,-3.173856984876353,-0.3819715353811027,5.729700533135147,-7.097541432816082,-3.6949973939179026,-0.11692806431143415,-3.895180491131896,2.0547548660462955,1.6979619539598476],\"layerTwoWeights\":[5.44573619851985,5.522509842250539,-4.9276948411470585]}";
    JsonNode? helloWorld = JsonNode.Parse(jsonString);
    Assert.AreEqual(0.20501795434531944, (double)helloWorld!["layerOneWeights"][0]);
  }
}
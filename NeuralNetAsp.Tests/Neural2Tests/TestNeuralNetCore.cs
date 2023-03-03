using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;

//https://www.automatetheplanet.com/mstest-cheat-sheet/
//https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
//https://www.kdnuggets.com/2019/11/build-artificial-neural-network-scratch-part-1.html
//https://towardsdatascience.com/building-a-neural-network-with-a-single-hidden-layer-using-numpy-923be1180dbf

[TestClass]
public class TestNeuralNetCore
{

  [TestMethod]
  public void testCreateNetworkParams()
  {
    var inputSize = 3;
    var hiddenSize = 2;
    var outputSize = 1;

    var parameters = new NetworkParameters(inputSize, hiddenSize, outputSize);

    Assert.AreEqual(parameters.weightsLayerOne.GetHeight(), hiddenSize);
    Assert.AreEqual(parameters.weightsLayerOne.GetWidth(), inputSize);

    Assert.AreEqual(parameters.biasLayerOne.GetHeight(), hiddenSize);
    Assert.AreEqual(parameters.biasLayerOne.GetWidth(), 1);

    Assert.AreEqual(parameters.weightsLayerTwo.GetHeight(), outputSize);
    Assert.AreEqual(parameters.weightsLayerTwo.GetWidth(), hiddenSize);

    Assert.AreEqual(parameters.biasLayerTwo.GetHeight(), outputSize);
    Assert.AreEqual(parameters.biasLayerTwo.GetWidth(), 1);
  }
}

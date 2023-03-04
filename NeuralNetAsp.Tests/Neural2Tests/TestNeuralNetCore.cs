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
  int inputSize;
  int hiddenSize;
  int outputSize;

  Matrix firstLayer;
  Matrix firstBias;
  Matrix secondLayer;
  Matrix secondBias;

  NetworkParameters parameters;


  public TestNeuralNetCore()
  {
    inputSize = 3;
    hiddenSize = 2;
    outputSize = 1;

    firstLayer = Matrix.generateOnesMatrix(hiddenSize, inputSize).times(0.1);
    firstBias = Matrix.generateOnesMatrix(hiddenSize, 1).times(0.2);
    secondLayer = Matrix.generateOnesMatrix(outputSize, hiddenSize).times(0.3);
    secondBias = Matrix.generateOnesMatrix(outputSize, 1).times(0.4);

    parameters = new NetworkParameters(firstLayer, firstBias, secondLayer, secondBias);
  }

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

  [TestMethod]
  public void testForwardPropagation()
  {

    var input = Matrix.generateOnesMatrix(inputSize, 1).times(0.5);

    var feedForwardResult = NeuralNetCore.feedForward(input, parameters);

    Assert.AreEqual(1, feedForwardResult.GetHeight());
    Assert.AreEqual(1, feedForwardResult.GetWidth());
    Assert.AreEqual(0.538347, feedForwardResult.Get(0, 0), 1e-5);

  }

  [TestMethod]
  public void testComputeCrossEntropyCost()
  {
    var answer = new MutableMatrix(1, 1);
    answer.Set(0, 0.538347);

    var expectedAnswer = new MutableMatrix(1, 1);
    expectedAnswer.Set(0, 3);

    var cost = NeuralNetCore.computeCrossEntropyCost(answer, expectedAnswer, parameters);
  }

}

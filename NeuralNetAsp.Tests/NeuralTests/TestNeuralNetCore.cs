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

  int numberOfTrainingCases;

  Matrix firstLayer;

  Matrix secondLayer;

  Matrix trainingDataInput;

  NetworkParameters parameters;


  public TestNeuralNetCore()
  {
    inputSize = 5;
    hiddenSize = 4;
    outputSize = 1;
    numberOfTrainingCases = 5;

    trainingDataInput = new Matrix(new double[,] {
      {-0.33, 0.69, 0, 1},
      {-0.33, 0.94, 1, 0},
      {-0.33, 0.5, 1, 0},
      {-0.33, 0.75, 0, 1},
      {-0.33, 0.67, 1, 1},
      });

    firstLayer = Matrix.generateRandomMatrix(hiddenSize, inputSize);
    secondLayer = Matrix.generateRandomMatrix(outputSize, hiddenSize);

    parameters = new NetworkParameters(firstLayer, secondLayer);
  }

  [TestMethod]
  public void testCreateNetworkParams()
  {
    var parameters = new NetworkParameters(inputSize, hiddenSize, outputSize);

    Assert.AreEqual(parameters.weightsLayerOne.GetHeight(), hiddenSize);
    Assert.AreEqual(parameters.weightsLayerOne.GetWidth(), inputSize);

    Assert.AreEqual(parameters.weightsLayerTwo.GetHeight(), outputSize);
    Assert.AreEqual(parameters.weightsLayerTwo.GetWidth(), hiddenSize);
  }

  [TestMethod]
  public void testForwardPropagationSingleStepSmallDataset()
  {

    var weightsOne = new Matrix(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });
    var input = new Matrix(new double[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
    var feedForwardResult = NeuralNetCore.feedForwardSingleLayer(input, weightsOne);

    Assert.AreEqual(2, feedForwardResult.GetWidth());
    Assert.AreEqual(3, feedForwardResult.GetHeight());

    Assert.AreEqual(0.6682, feedForwardResult.Get(0), 1e-4);
    Assert.AreEqual(0.8176, feedForwardResult.Get(1), 1e-4);
    Assert.AreEqual(0.9089, feedForwardResult.Get(2), 1e-4);
    Assert.AreEqual(0.7311, feedForwardResult.Get(3), 1e-4);
    Assert.AreEqual(0.9002, feedForwardResult.Get(4), 1e-4);
    Assert.AreEqual(0.9677, feedForwardResult.Get(5), 1e-4);
  }

  [TestMethod]
  public void testForwardPropagationSampleDataSet()
  {

    var weightsOne = new Matrix(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });
    var input = new Matrix(new double[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
    var feedForwardResult = NeuralNetCore.feedForwardSingleLayer(input, weightsOne);

    Assert.AreEqual(2, feedForwardResult.GetWidth());
    Assert.AreEqual(3, feedForwardResult.GetHeight());

    Assert.AreEqual(0.6682, feedForwardResult.Get(0), 1e-4);
    Assert.AreEqual(0.8176, feedForwardResult.Get(1), 1e-4);
    Assert.AreEqual(0.9089, feedForwardResult.Get(2), 1e-4);
    Assert.AreEqual(0.7311, feedForwardResult.Get(3), 1e-4);
    Assert.AreEqual(0.9002, feedForwardResult.Get(4), 1e-4);
    Assert.AreEqual(0.9677, feedForwardResult.Get(5), 1e-4);
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

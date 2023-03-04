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

    firstLayer = new Matrix(new double[,] {
      {-0.1660, -0.7065, -0.2065},
      {0.4406, -0.8153, 0.0776},
      {-0.9998, -0.6275, -0.1616},
      {-0.3953, -0.3089, 0.3704}
    });

    secondLayer = new Matrix(new double[,] {
      {-0.5911},
      {0.7562},
      {-0.9452}
    });

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
  public void testForwardSingleLayerSampleDataSet()
  {
    var feedForwardResult = NeuralNetCore.feedForwardSingleLayer(trainingDataInput, firstLayer);

    Assert.AreEqual(3, feedForwardResult.GetWidth());
    Assert.AreEqual(5, feedForwardResult.GetHeight());

    Assert.AreEqual(0.4909, feedForwardResult.Get(0, 0), 1e-4);
    Assert.AreEqual(0.2602, feedForwardResult.Get(4, 0), 1e-4);
    Assert.AreEqual(0.5815, feedForwardResult.Get(4, 2), 1e-4);

  }

  [TestMethod]
  public void testForwardDoubleLayerSampleDataSet()
  {
    var feedForwardResult = NeuralNetCore.feedForward(trainingDataInput, firstLayer, secondLayer);

    Assert.AreEqual(1, feedForwardResult.GetWidth());
    Assert.AreEqual(5, feedForwardResult.GetHeight());

    Assert.AreEqual(0.3508, feedForwardResult.Get(0), 1e-4);
    Assert.AreEqual(0.3694, feedForwardResult.Get(4), 1e-4);
  }

  [TestMethod]
  public void testComputeLayerDeltaSmallDataset()
  {
    var error = new Matrix(new double[,]{
      {1, 2},
      {3, 4}
    });

    var layer = new Matrix(new double[,]{
      {1, 2},
      {3, 4}
    });

    Matrix delta = NeuralNetCore.computeDelta(error, layer);

    Assert.AreEqual(2, delta.GetHeight());
    Assert.AreEqual(2, delta.GetWidth());

    Assert.AreEqual(0.1966, delta.Get(0, 0), 1e-4);
    Assert.AreEqual(0.1355, delta.Get(1, 0), 1e-4);
    Assert.AreEqual(0.2100, delta.Get(0, 1), 1e-4);
    Assert.AreEqual(0.0707, delta.Get(1, 1), 1e-4);

  }

}

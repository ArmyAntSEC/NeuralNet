using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Models.Neural;
using NeuralNetAsp.Models.MatrixCore;

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

  double alpha;

  double errorTolerance;

  int numberOfIterations;

  Matrix firstLayerWeights;

  Matrix secondLayerWeights;

  Matrix trainingDataInput;
  Matrix trainingDataOutput;

  NetworkParameters parameters;


  public TestNeuralNetCore()
  {
    inputSize = 5;
    hiddenSize = 4;
    outputSize = 1;

    alpha = 0.001;

    numberOfIterations = 1000000;

    errorTolerance = 0.05;

    trainingDataInput = new Matrix(new double[,] {
      {-0.33, 0.69, 0, 1},
      {-0.33, 0.94, 1, 0},
      {-0.33, 0.5, 1, 0},
      {-0.33, 0.75, 0, 1},
      {-0.33, 0.67, 1, 1},
      });

    trainingDataOutput = new Matrix(new double[,] {
      {1},
      {1},
      {0},
      {1},
      {0}
    });

    firstLayerWeights = new Matrix(new double[,] {
      {-0.1660, -0.7065, -0.2065},
      {0.4406, -0.8153, 0.0776},
      {-0.9998, -0.6275, -0.1616},
      {-0.3953, -0.3089, 0.3704}
    });

    secondLayerWeights = new Matrix(new double[,] {
      {-0.5911},
      {0.7562},
      {-0.9452}
    });

    parameters = new NetworkParameters(firstLayerWeights, secondLayerWeights);
  }

  [TestMethod]
  public void testCreateNetworkParams()
  {
    var parameters = new NetworkParameters(inputSize, hiddenSize, outputSize);

    Assert.AreEqual(parameters.WeightsLayerOne.GetHeight(), hiddenSize);
    Assert.AreEqual(parameters.WeightsLayerOne.GetWidth(), inputSize);

    Assert.AreEqual(parameters.WeightsLayerTwo.GetHeight(), outputSize);
    Assert.AreEqual(parameters.WeightsLayerTwo.GetWidth(), hiddenSize);
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
  public void testForwardDoubleLayerSampleDataSet()
  {
    var feedForwardResult = NeuralNetCore.feedForward(trainingDataInput, parameters);

    Assert.AreEqual(3, feedForwardResult.LayerOne.GetWidth());
    Assert.AreEqual(5, feedForwardResult.LayerOne.GetHeight());

    Assert.AreEqual(0.4908, feedForwardResult.LayerOne.Get(0), 1e-4);
    Assert.AreEqual(0.5815, feedForwardResult.LayerOne.Get(4, 2), 1e-4);

    Assert.AreEqual(1, feedForwardResult.LayerTwo.GetWidth());
    Assert.AreEqual(5, feedForwardResult.LayerTwo.GetHeight());

    Assert.AreEqual(0.3508, feedForwardResult.LayerTwo.Get(0), 1e-4);
    Assert.AreEqual(0.3694, feedForwardResult.LayerTwo.Get(4), 1e-4);
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

  [TestMethod]
  public void testComputeBothLayerDeltaTestDataset()
  {
    var feedForwardResult = NeuralNetCore.feedForward(trainingDataInput, parameters);

    var deltas = NeuralNetCore.computeDeltas(feedForwardResult, parameters, trainingDataOutput);

    Assert.AreEqual(5, deltas.LayerOneDelta.GetHeight());
    Assert.AreEqual(3, deltas.LayerOneDelta.GetWidth());

    Assert.AreEqual(0.0219, deltas.LayerOneDelta.Get(0, 0), 1e-4);
    Assert.AreEqual(-0.0130, deltas.LayerOneDelta.Get(4, 0), 1e-4);
    Assert.AreEqual(-0.0194, deltas.LayerOneDelta.Get(4, 2), 1e-4);

    Assert.AreEqual(5, deltas.LayerTwoDelta.GetHeight());
    Assert.AreEqual(1, deltas.LayerTwoDelta.GetWidth());

    Assert.AreEqual(-0.1574, deltas.LayerTwoDelta.Get(0, 0), 1e-4);
    Assert.AreEqual(0.0893, deltas.LayerTwoDelta.Get(4, 0), 1e-4);

    Assert.AreEqual(0.5383, deltas.ErrorVal, 1e-4);
  }

  [TestMethod]
  public void testUpdateWeightsSmallDataset()
  {
    var weights = new Matrix(new double[,]{
      {1, 2},
      {3, 4}
    });

    var alpha = 0.001;

    var layer = new Matrix(new double[,]{
      {1, 2},
      {3, 4},
      {5, 6}
    });

    var layer2_delta = new Matrix(new double[,]{
      {1, 2},
      {3, 4},
      {5, 6}
    });

    Matrix newWeights = NeuralNetCore.computenewWeightsSingleLayer(weights, alpha, layer, layer2_delta);

    Assert.AreEqual(weights.GetHeight(), newWeights.GetHeight());
    Assert.AreEqual(weights.GetWidth(), newWeights.GetWidth());

    Assert.AreEqual(0.9650, newWeights.Get(0, 0), 1e-4);
    Assert.AreEqual(2.9560, newWeights.Get(1, 0), 1e-4);
    Assert.AreEqual(1.9560, newWeights.Get(0, 1), 1e-4);
    Assert.AreEqual(3.9440, newWeights.Get(1, 1), 1e-4);

  }


  [TestMethod]
  public void testComputeAllweightCorrectionsTestDataset()
  {
    var feedForwardResult = NeuralNetCore.feedForward(trainingDataInput, parameters);

    var deltas = NeuralNetCore.computeDeltas(feedForwardResult, parameters, trainingDataOutput);

    var updatedParams = NeuralNetCore.computeNewWeights(parameters, trainingDataInput, alpha, feedForwardResult, deltas);

    Assert.AreEqual(parameters.WeightsLayerOne.GetHeight(), updatedParams.WeightsLayerOne.GetHeight());
    Assert.AreEqual(parameters.WeightsLayerOne.GetWidth(), updatedParams.WeightsLayerOne.GetWidth());

    Assert.AreEqual(parameters.WeightsLayerTwo.GetHeight(), updatedParams.WeightsLayerTwo.GetHeight());
    Assert.AreEqual(parameters.WeightsLayerTwo.GetWidth(), updatedParams.WeightsLayerTwo.GetWidth());

    Assert.AreEqual(-0.1659, updatedParams.WeightsLayerOne.Get(0, 0), 1e-4);
    Assert.AreEqual(-0.3954, updatedParams.WeightsLayerOne.Get(3, 0), 1e-4);
    Assert.AreEqual(0.3704, updatedParams.WeightsLayerOne.Get(3, 2), 1e-4);

    Assert.AreEqual(-0.5909, updatedParams.WeightsLayerTwo.Get(0, 0), 1e-4);
    Assert.AreEqual(-0.9451, updatedParams.WeightsLayerTwo.Get(2, 0), 1e-4);
  }

  [TestMethod]
  public void testEntireTrainingOnTestData()
  {
    NetworkParameters finalParameters = NeuralNetCore.TrainNetwork(trainingDataInput, trainingDataOutput, alpha, numberOfIterations, errorTolerance, parameters);

    Assert.AreEqual(0.2051, finalParameters.WeightsLayerOne.Get(0, 0), 1e-4);
    Assert.AreEqual(-3.1739, finalParameters.WeightsLayerOne.Get(3, 0), 1e-4);
    Assert.AreEqual(1.6980, finalParameters.WeightsLayerOne.Get(3, 2), 1e-4);

    Assert.AreEqual(5.4458, finalParameters.WeightsLayerTwo.Get(0, 0), 1e-4);
    Assert.AreEqual(-4.9277, finalParameters.WeightsLayerTwo.Get(2, 0), 1e-4);

  }

}

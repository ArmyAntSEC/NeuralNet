using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Models.Neural;
using NeuralNetAsp.Models.MatrixCore;

namespace NeuralNetAsp.UnitTests.Neural;

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
    var param = new TrainingDataSetComplete(
        trainingDataInput, trainingDataOutput, alpha,
        numberOfIterations, errorTolerance, parameters);
    NetworkParameters finalParameters = NeuralNetCore.TrainNetwork(param);

    Assert.AreEqual(0.2051, finalParameters.WeightsLayerOne.Get(0, 0), 1e-4);
    Assert.AreEqual(-3.1739, finalParameters.WeightsLayerOne.Get(3, 0), 1e-4);
    Assert.AreEqual(1.6980, finalParameters.WeightsLayerOne.Get(3, 2), 1e-4);

    Assert.AreEqual(5.4458, finalParameters.WeightsLayerTwo.Get(0, 0), 1e-4);
    Assert.AreEqual(-4.9277, finalParameters.WeightsLayerTwo.Get(2, 0), 1e-4);

  }

}

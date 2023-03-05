using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Models.Neural;
using NeuralNetAsp.Models.MatrixCore;

namespace NeuralNetAsp.UnitTests.Neural;

[TestClass]
public class TestNeuralNetComponents
{
  [TestMethod]
  public void testCreateNetworkParams()
  {

    var inputSize = 5;
    var hiddenSize = 4;
    var outputSize = 1;
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


}
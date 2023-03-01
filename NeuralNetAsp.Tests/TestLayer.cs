using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;

namespace NeuralNetAsp.Tests;

[TestClass]
public class TestLayer
{
  //Each layer has N perceptrons

  //There exists a fake alyer that is just the input

  //A Layer takes another layer as its input.

  //Every perceptron in the layer reads the output array of the input layer and computes its own output.

  [TestMethod]
  public void TestCreateInputLayer()
  {

    var input = new double[] { 1, 2, 3 };

    var sut = new InputLayer();
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(input);

    Assert.AreEqual(input, sut.GetOutput());

  }

  [TestMethod]
  public void TestCreateComputingLayerSingleInputAndOutput()
  {
    var weights = new List<double[]> { new double[] { 1.0 } };
    var offsets = new double[] { -1.0 };
    var inputs = new double[] { 1.0 };
    var output = 0.5;

    var sut = new ComputingLayer(weights, offsets);
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(inputs);

    Assert.AreEqual(output, sut.GetOutput()[0]);

  }

  [TestMethod]
  public void TestCreateComputingLayerDoubleInputAndSingleOutput()
  {
    var weights = new List<double[]> { new double[] { 1.0, 2.0 } };
    var inputs = new double[] { 1.0, 2.0 };
    var offsets = new double[] { 0.0 };
    var output = 0.99331;

    var sut = new ComputingLayer(weights, offsets);
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(inputs);

    Assert.AreEqual(output, sut.GetOutput()[0], 1e-5);

  }

}
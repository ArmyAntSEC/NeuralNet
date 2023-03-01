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
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0 }, -1 )
    };

    var inputs = new double[] { 1.0 };
    var output = 0.5;

    var sut = new ComputingLayer(definitions);
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(inputs);

    Assert.AreEqual(output, sut.GetOutput()[0]);

  }

  [TestMethod]
  public void TestCreateComputingLayerDoubleInputAndSingleOutput()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0, 2.0 }, -0 )
    };

    var inputs = new double[] { 1.0, 2.0 };
    var output = 0.99331;

    var sut = new ComputingLayer(definitions);
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(inputs);

    Assert.AreEqual(output, sut.GetOutput()[0], 1e-5);

  }

  [TestMethod]
  public void TestCreateComputingLayerSingleInputAndDoubleOutput()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0 }, -1 ),
      new PerceptronDefinition( new double[] { 2.0 }, 3 )
    };

    var inputs = new double[] { 1 };
    var outputs = new double[] { 0.5, 0.99331 };

    var sut = new ComputingLayer(definitions);
    Assert.IsInstanceOfType(sut, typeof(ILayerBase));

    sut.SetInputs(inputs);

    Assert.AreEqual(outputs[0], sut.GetOutput()[0], 1e-5);
    Assert.AreEqual(outputs[1], sut.GetOutput()[1], 1e-5);

  }

}
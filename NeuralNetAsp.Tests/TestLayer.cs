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
    Assert.IsInstanceOfType(sut, typeof(LayerBase));

    sut.SetValues(input);

    Assert.AreEqual(input, sut.GetOutput());

  }

}
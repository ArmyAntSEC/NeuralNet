using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;

namespace NeuralNetAsp.Tests;

[TestClass]
public class TestLayer
{
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

    Assert.AreEqual(output, sut.FeedForward(inputs)[0]);

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

    Assert.AreEqual(output, sut.FeedForward(inputs)[0], 1e-5);

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

    var returnValue = sut.FeedForward(inputs);
    Assert.AreEqual(outputs[0], returnValue[0], 1e-5);
    Assert.AreEqual(outputs[1], returnValue[1], 1e-5);

  }

  [TestMethod]
  [ExpectedException(typeof(ArgumentException))]
  public void TestCannotCreateLayerWithdifferentlySizedPerceptrons()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0 }, -1 ),
      new PerceptronDefinition( new double[] { 2.0, 2 }, 3 )
    };

    var sut = new ComputingLayer(definitions);
  }

  [TestMethod]
  public void TestGetInputAndOutputSize()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0, 2.0, 3.0 }, -1 ),
      new PerceptronDefinition( new double[] { 2.0, 2.0, 3.0 }, 3 )
    };

    var sut = new ComputingLayer(definitions);

    Assert.AreEqual(3, sut.GetInputSize());
    Assert.AreEqual(2, sut.GetOutputSize());
  }

  [TestMethod]
  [ExpectedException(typeof(ArgumentException))]
  public void TestSendWrongSizeInput()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0, 2.0 }, -1 )
    };

    var sut = new ComputingLayer(definitions);

    sut.FeedForward(new double[] { 1.0 });
  }


}
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;

namespace NeuralNetAsp.Tests;

[TestClass]
public class TestNetwork
{
  [TestMethod]
  public void TestNetworkSingleLayer()
  {
    var definitions = new List<PerceptronDefinition>() {
      new PerceptronDefinition( new double[] { 1.0, 2.0 }, -0 )
    };

    var inputs = new double[] { 1.0, 2.0 };
    var output = 0.99331;

    var layer = new ComputingLayer(definitions);
    var layers = new ILayerBase[] { layer };

    var sut = new Network(layers);

    Assert.AreEqual(output, sut.GetOutput(inputs)[0], 1e-5);

  }

  [TestMethod]
  public void TestNetworkDoubleLayer()
  {
    var layerOne = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 1.0, -2.0 }, 3 )
      }
    );

    var layerTwo = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 2.0 }, -1 )
      }
    );

    var layers = new ILayerBase[] { layerOne, layerTwo };

    var inputs = new double[] { 1.0, 2.0 };
    var output = 0.5;

    var sut = new Network(layers);

    Assert.AreEqual(output, sut.GetOutput(inputs)[0], 1e-5);

  }

  [TestMethod]
  public void TestNetworkDoubleDoubleLayer()
  {
    var layerOne = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 1.0, -2.0 }, 3 ),
        new PerceptronDefinition( new double[] { 1.0, -2.0 }, 3 )
      }
    );

    var layerTwo = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 1.0, 1.0 }, -1 )
      }
    );

    var layers = new ILayerBase[] { layerOne, layerTwo };

    var inputs = new double[] { 1.0, 2.0 };
    var output = 0.5;

    var sut = new Network(layers);

    Assert.AreEqual(output, sut.GetOutput(inputs)[0], 1e-5);

  }

  [TestMethod]
  public void TestNetworkDoubleLayerNotMatching()
  {
    var layerOne = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 1.0, -2.0 }, 3 ),
        new PerceptronDefinition( new double[] { 1.0, -2.0 }, 3 )
      }
    );

    var layerTwo = new ComputingLayer(
      new List<PerceptronDefinition>() {
        new PerceptronDefinition( new double[] { 1.0 }, -1 ),
        new PerceptronDefinition( new double[] { 1.0 }, -1 )
      }
    );

    var layers = new ILayerBase[] { layerOne, layerTwo };


    var sut = new Network(layers);
  }
}
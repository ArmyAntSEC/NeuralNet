using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;

//https://www.automatetheplanet.com/mstest-cheat-sheet/
//https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
//https://www.kdnuggets.com/2019/11/build-artificial-neural-network-scratch-part-1.html
//https://towardsdatascience.com/building-a-neural-network-with-a-single-hidden-layer-using-numpy-923be1180dbf

[TestClass]
public class TestPerceptron
{

  [TestMethod]
  [DataRow(0, 0.5)]
  [DataRow(5, 0.99331)]
  [DataRow(-5, 0.00669)]
  public void TestActivationFunction(double inputValue, double expectedValue)
  {
    Assert.AreEqual(expectedValue, Perceptron.ActivationFunction(inputValue), 1e-5);
  }

  [TestMethod]
  [DataRow(5, 0.00665)]
  [DataRow(-5, 0.00665)]
  public void TestActivationFunctionDerivative(double inputValue, double expectedValue)
  {
    Assert.AreEqual(expectedValue, Perceptron.ActivationFunctionDerivative(inputValue), 1e-5);
  }

  [TestMethod]
  [DataRow(new double[] { 2 }, new double[] { 2 }, 4)]
  [DataRow(new double[] { 1 }, new double[] { 1 }, 1)]
  [DataRow(new double[] { 1, 2 }, new double[] { 1, 2 }, 5)]
  public void TestDotProduct(double[] a, double[] b, double c)
  {
    Assert.AreEqual(c, Perceptron.DotProduct(a, b));
  }

  /*
  [TestMethod]
  public void TestPerceptronFeedForward()
  {
    var weights = new double[2] { 1, 2 };
    var inputs = new double[2] { 1, -1 };
    var bias = -1;

    var sut = new Perceptron(weights, bias);

    var actualValue = sut.FeedForward(inputs);

    Assert.AreEqual(0.5, actualValue);

  }
  */
}
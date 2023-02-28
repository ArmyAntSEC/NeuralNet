using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;

//https://www.automatetheplanet.com/mstest-cheat-sheet/
//https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
//https://www.kdnuggets.com/2019/11/build-artificial-neural-network-scratch-part-1.html

[TestClass]
public class TestNode
{

  [TestMethod]
  [DataRow(0, 0.5)]
  [DataRow(5, 0.99331)]
  [DataRow(-5, 0.00669)]
  public void TestActivationFunction(double inputValue, double expectedValue)
  {
    Assert.AreEqual(expectedValue, Node.ActivationFunction(inputValue), 1e-5);
  }

  [TestMethod]
  [DataRow(5, 0.00665)]
  [DataRow(-5, 0.00665)]
  public void TestActivationFunctionDerivative(double inputValue, double expectedValue)
  {
    Assert.AreEqual(expectedValue, Node.ActivationFunctionDerivative(inputValue), 1e-5);
  }
}
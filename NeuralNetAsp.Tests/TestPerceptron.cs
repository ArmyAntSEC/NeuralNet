using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;

//https://www.automatetheplanet.com/mstest-cheat-sheet/

[TestClass]
public class UnitTest1
{

  [TestMethod]
  public void TestActivationFunction()
  {

    var inputValue = 0;

    Assert.AreEqual(Perceptron.activationFunction(inputValue), 0);

  }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;


[TestClass]
public class UnitTest1
{

  [TestMethod]
  public void Test1()
  {
    var sut = new Perceptron();

    Assert.AreEqual(3, 2);

  }
}
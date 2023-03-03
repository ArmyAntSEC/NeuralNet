using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;

[TestClass]
public class TestMathCore
{

  [TestMethod]
  [DataRow(0, 0.5)]
  [DataRow(5, 0.99331)]
  [DataRow(-5, 0.00669)]
  public void TestSigmoidFunction(double inputValue, double expectedValue)
  {
    Assert.AreEqual(expectedValue, MathCore.SigmoidFunction(inputValue), 1e-5);
  }


  [TestMethod]
  [DataRow(new double[] { 2 }, new double[] { 2 }, 4)]
  [DataRow(new double[] { 1 }, new double[] { 1 }, 1)]
  [DataRow(new double[] { 1, 2 }, new double[] { 1, 2 }, 5)]
  public void TestDotProduct(double[] a, double[] b, double c)
  {
    Assert.AreEqual(c, MathCore.DotProduct(a, b), 1e-5);
  }

  [TestMethod]
  [DataRow(new double[] { 2 }, new double[] { 2, 2 })]
  [ExpectedException(typeof(ArgumentException))]
  public void TestDotProductWithBadSizeInput(double[] a, double[] b)
  {
    MathCore.DotProduct(a, b);
  }

  [TestMethod]
  public void testGenerateRandomMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = MathCore.generateRandomMatrix(height, width);
    Assert.AreEqual(matrix.GetLength(0), height);
    Assert.AreEqual(matrix.GetLength(1), width);

    var matrix2 = MathCore.generateRandomMatrix(height, width);
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreNotEqual(matrix[j, i], 0);
        Assert.AreNotEqual(matrix[j, i], matrix2[j, i]);
      }
    }
  }

}
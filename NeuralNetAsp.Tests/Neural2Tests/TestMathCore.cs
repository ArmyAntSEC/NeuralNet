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
  public void TestDotProductColumns()
  {
    var matrixOne = new MutableMatrix(1, 3);
    var matrixTwo = new MutableMatrix(3, 1);
    for (var i = 0; i < 3; i++)
    {
      matrixOne.Set(i, i);
      matrixTwo.Set(i, i);
    }

    var result = matrixOne.dot(matrixTwo);
    Assert.AreEqual(5, result.Get(0, 0), 1e-5);
  }

  [TestMethod]
  public void TestDotProductColumnAndRow()
  {

    var matrixOne = new MutableMatrix(2, 3);
    var matrixTwo = new MutableMatrix(3, 1);

    for (int i = 0; i < 6; i++)
    {
      matrixOne.Set(i, i);
      if (i < 3)
      {
        matrixTwo.Set(i, i);
      }
    }

    var matrixThree = matrixOne.dot(matrixTwo);

    Assert.AreEqual(10, matrixThree.Get(0, 0));
    Assert.AreEqual(13, matrixThree.Get(1, 0));
  }

  [TestMethod]
  [ExpectedException(typeof(ArgumentException))]
  public void TestDotProductWithBadSizeInput()
  {
    Matrix.generateRandomMatrix(2, 1).dot(Matrix.generateRandomMatrix(3, 1));
  }

  [TestMethod]
  public void TestSetWithLinearIdx()
  {
    var height = 3;
    var width = 2;
    var matrix = new MutableMatrix(Matrix.generateZeroMatrix(height, width));

    matrix.Set(1, 3);
    Assert.AreEqual(3, matrix.Get(1, 0));

    matrix.Set(4, 2);
    Assert.AreEqual(2, matrix.Get(1, 1));
  }

  [TestMethod]
  public void testGenerateRandomMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateRandomMatrix(height, width);
    Assert.AreEqual(matrix.GetHeight(), height);
    Assert.AreEqual(matrix.GetWidth(), width);

    var matrix2 = Matrix.generateRandomMatrix(height, width);
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreNotEqual(matrix.Get(j, i), 0);
        Assert.AreNotEqual(matrix.Get(j, i), matrix2.Get(j, i));
      }
    }
  }

  [TestMethod]
  public void testGenerateZeroMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateZeroMatrix(height, width);

    Assert.AreEqual(matrix.GetHeight(), height);
    Assert.AreEqual(matrix.GetWidth(), width);

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreEqual(matrix.Get(j, i), 0);
      }
    }
  }

  [TestMethod]
  public void testGenerateOnesMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateOnesMatrix(height, width);

    Assert.AreEqual(matrix.GetHeight(), height);
    Assert.AreEqual(matrix.GetWidth(), width);

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreEqual(matrix.Get(j, i), 1);
      }
    }
  }

  [TestMethod]
  public void testCopyMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateRandomMatrix(height, width);
    var matrix2 = new Matrix(matrix);

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreEqual(matrix2.Get(j, i), matrix.Get(j, i), 1e-5);
      }
    }
  }

  [TestMethod]
  public void testCreateMutableMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateRandomMatrix(height, width);

    var matrix2 = new MutableMatrix(matrix);

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        var thisValue = i * j;
        matrix2.Set(j, i, thisValue);
        Assert.AreEqual(matrix2.Get(j, i), thisValue, 1e-5);
      }
    }
  }

  [TestMethod]
  public void testScalarMultiplication()
  {
    var height = 2;
    var width = 3;
    var factor = 2.0;

    var matrix = Matrix.generateRandomMatrix(height, width);
    var matrix2 = matrix.times(factor);

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        Assert.AreEqual(matrix2.Get(j, i), factor * matrix.Get(j, i), 1e-5);
      }
    }
  }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Models.MatrixCore;

namespace NeuralNetAsp.UnitTests.MatrixCore.Creators;

[TestClass]
public class TestMatrixCreators
{
  [TestMethod]
  public void TestCreateFromArray()
  {
    var data = new double[] { 1, 2, 3, 4 };
    var matrix = new Matrix(data, 2, 2);

    Assert.AreEqual(2, matrix.GetHeight());
    Assert.AreEqual(2, matrix.GetWidth());

    Assert.AreEqual(1, matrix.Get(0, 0));
    Assert.AreEqual(2, matrix.Get(1, 0));
    Assert.AreEqual(3, matrix.Get(0, 1));
    Assert.AreEqual(4, matrix.Get(1, 1));

  }

  [TestMethod]
  public void testGenerateRandomMatrix()
  {
    var height = 3;
    var width = 2;

    var matrix = Matrix.generateRandomMatrix(height, width, 1);
    Assert.AreEqual(matrix.GetHeight(), height);
    Assert.AreEqual(matrix.GetWidth(), width);

    var matrix2 = Matrix.generateRandomMatrix(height, width, 2);
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
  public void testCreateFromRectangularArray()
  {
    var height = 2;
    var width = 3;
    var data = new double[2, 3] { { 1, 3, 5 }, { 2, 4, 6 } };

    var matrix = new Matrix(data);

    var elements = width * height;
    for (int i = 0; i < elements; i++)
    {
      Assert.AreEqual(i + 1, matrix.Get(i), 1e-5);
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


}
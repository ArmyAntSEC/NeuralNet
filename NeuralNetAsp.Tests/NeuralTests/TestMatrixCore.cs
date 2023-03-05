using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Model.MatrixCore;
namespace NeuralNetAsp.Tests;

[TestClass]
public class TestMatrixCore
{
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
    Assert.AreEqual(3, matrix.Get(1));

    matrix.Set(4, 2);
    Assert.AreEqual(2, matrix.Get(1, 1));
    Assert.AreEqual(2, matrix.Get(4));
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

  [TestMethod]
  public void testMatrixAddition()
  {
    var height = 2;
    var width = 3;

    var matrix = new MutableMatrix(height, width);
    var matrix2 = new MutableMatrix(height, width);
    for (int i = 0; i < height * width; i++)
    {
      matrix.Set(i, i);
      matrix2.Set(i, i);
    }

    var matrix3 = matrix.plus(matrix2);

    for (int i = 0; i < width * height; i++)
    {
      Assert.AreEqual(2 * i, matrix3.Get(i), 1e-5);
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
  public void testMultiplyMatricesScalaerResult()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });
    var data2 = new Matrix(new double[2, 1] { { 1 }, { 3 } });

    Matrix result = data.mtimes(data2);

    Assert.AreEqual(1, result.GetWidth());
    Assert.AreEqual(1, result.GetHeight());
    Assert.AreEqual(10, result.Get(0));
  }

  [TestMethod]
  public void testMultiplyMatricesMatrixResult()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });
    var data2 = new Matrix(new double[2, 1] { { 1 }, { 3 } });

    Matrix result = data2.mtimes(data);

    Assert.AreEqual(2, result.GetWidth());
    Assert.AreEqual(2, result.GetHeight());

    Assert.AreEqual(1, result.Get(0));
    Assert.AreEqual(3, result.Get(1));
    Assert.AreEqual(3, result.Get(2));
    Assert.AreEqual(9, result.Get(3));
  }

  [TestMethod]
  public void testExponential()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });

    Matrix result = data.exp();

    Assert.AreEqual(2, result.GetWidth());
    Assert.AreEqual(1, result.GetHeight());

    Assert.AreEqual(2.7183, result.Get(0), 1e-4);
    Assert.AreEqual(20.0855, result.Get(1), 1e-4);

  }

  [TestMethod]
  public void testAddScalar()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });

    Matrix result = data.plus(2);

    Assert.AreEqual(2, result.GetWidth());
    Assert.AreEqual(1, result.GetHeight());

    Assert.AreEqual(3, result.Get(0));
    Assert.AreEqual(5, result.Get(1));

  }


  [TestMethod]
  public void testElementWizePower()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });

    Matrix result = data.elementPower(2);

    Assert.AreEqual(2, result.GetWidth());
    Assert.AreEqual(1, result.GetHeight());

    Assert.AreEqual(1, result.Get(0));
    Assert.AreEqual(9, result.Get(1));

  }

  [TestMethod]
  public void testElementWizeMult()
  {
    var data = new Matrix(new double[1, 2] { { 1, 3 } });

    var data2 = new Matrix(new double[1, 2] { { 2, 4 } });

    Matrix result = data.elementMult(data2);

    Assert.AreEqual(2, result.GetWidth());
    Assert.AreEqual(1, result.GetHeight());

    Assert.AreEqual(2, result.Get(0));
    Assert.AreEqual(12, result.Get(1));

  }


  [TestMethod]
  public void testTranspose()
  {
    var data = new Matrix(new double[3, 2] { { 1, 4 }, { 2, 5 }, { 3, 6 } });

    Matrix result = data.transpose();

    Assert.AreEqual(2, result.GetHeight());
    Assert.AreEqual(3, result.GetWidth());

    Assert.AreEqual(1, result.Get(0, 0));
    Assert.AreEqual(2, result.Get(0, 1));
    Assert.AreEqual(3, result.Get(0, 2));
    Assert.AreEqual(4, result.Get(1, 0));
    Assert.AreEqual(5, result.Get(1, 1));
    Assert.AreEqual(6, result.Get(1, 2));

  }

  [TestMethod]
  public void testAbs()
  {
    var data = new Matrix(new double[,] { { 1, -4 } });

    Matrix result = data.abs();

    Assert.AreEqual(1, result.GetHeight());
    Assert.AreEqual(2, result.GetWidth());

    Assert.AreEqual(1, result.Get(0));
    Assert.AreEqual(4, result.Get(1));

  }

  [TestMethod]
  public void testMean()
  {
    var data = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });

    Matrix result = data.mean();

    Assert.AreEqual(1, result.GetHeight());
    Assert.AreEqual(2, result.GetWidth());

    Assert.AreEqual(2, result.Get(0));
    Assert.AreEqual(3, result.Get(1));

  }

}
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuralNetAsp.Models.MatrixCore;

namespace NeuralNetAsp.UnitTests.MatrixCore;

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
  public void testMultiplyMatricesScalarResult()
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
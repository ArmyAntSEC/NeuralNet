using System;

namespace NeuralNetAsp.Neural
{
  public class Matrix
  {
    private double[,] data;

    public Matrix(int height, int width)
    {
      data = new double[height, width];
    }

    public double Get(int row, int column)
    {
      return data[row, column];
    }

    public double GetWidth()
    {
      return data.GetLength(1);
    }

    public double GetHeight()
    {
      return data.GetLength(0);
    }

    public static Matrix generateRandomMatrix(int height, int width)
    {
      var randomGenerator = Medallion.Rand.Create();
      var rValue = new Matrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.data[j, i] = Medallion.Rand.NextGaussian(randomGenerator);
        }
      }
      return rValue;
    }

    public static Matrix generateZeroMatrix(int height, int width)
    {
      return new Matrix(height, width);
    }
  }
}
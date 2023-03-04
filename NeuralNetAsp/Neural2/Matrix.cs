using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  //This class is designed to not have any inplace operators, meaning the udnerlying data storage becomes read-only.
  public class Matrix
  {

    protected readonly double[,] data;

    public Matrix() : this(0, 0)
    { }

    public Matrix(int height, int width)
    {
      data = new double[height, width];
    }

    public Matrix(double[] input) : this(input.Length, 1)
    {
      var height = GetHeight();

      for (int j = 0; j < height; j++)
      {
        data[j, 0] = input[j];
      }
    }


    public Matrix(Matrix m)
    {
      var height = m.GetHeight();
      var width = m.GetWidth();

      data = new double[height, width];
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          data[j, i] = m.Get(j, i);
        }
      }
    }

    public double Get(int row, int column)
    {
      return data[row, column];
    }

    public int GetWidth()
    {
      return data.GetLength(1);
    }

    public int GetHeight()
    {
      return data.GetLength(0);
    }

    public double Get(int idx)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();

      return this.data[row, column];
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

    public static Matrix generateOnesMatrix(int height, int width)
    {
      var rValue = new Matrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.data[j, i] = 1;
        }
      }
      return rValue;
    }


    public Matrix times(double factor)
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, this.Get(j, i) * factor);
        }
      }
      return rValue;
    }

    public Matrix plus(Matrix term)
    {
      var height = GetHeight();
      var width = GetWidth();
      CheckEqual(height, term.GetHeight());
      CheckEqual(width, term.GetWidth());

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, this.Get(j, i) + term.Get(j, i));
        }
      }
      return rValue;
    }

    public Matrix tanh()
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width * height; i++)
      {
        rValue.Set(i, Math.Tanh(this.Get(i)));
      }
      return rValue;
    }

    public Matrix dot(Matrix b)
    {
      CheckEqual(GetWidth(), b.GetHeight());
      CheckEqual(b.GetWidth(), 1);


      var rValue = new MutableMatrix(GetHeight(), 1);
      for (int j = 0; j < GetHeight(); j++)
      {
        var sum = 0.0;
        for (int i = 0; i < b.GetHeight(); i++)
        {
          sum += Get(j, i) * b.Get(i, 0);
        }
        rValue.Set(j, 0, sum);
      }
      return rValue;
    }
  }

  public class MutableMatrix : Matrix
  {
    public MutableMatrix(int height, int width) : base(height, width)
    {
    }

    public MutableMatrix(Matrix matrix) : base(matrix)
    {

    }

    public void Set(int row, int column, double value)
    {
      this.data[row, column] = value;
    }

    public void Set(int idx, double value)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();

      this.data[row, column] = value;
    }
  }
}

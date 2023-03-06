using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Models.MatrixCore
{
  //This class is designed to not have any inplace operators, meaning the udnerlying data storage becomes read-only.  
  public class Matrix
  {

    private double[,] data;

    protected double[,] Data
    {
      get { return data; }
    }

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

    public double[] ToArray()
    {
      var rValue = new double[GetWidth() * GetHeight()];
      for (int i = 0; i < GetWidth() * GetHeight(); i++)
      {
        rValue[i] = Get(i);
      }
      return rValue;
    }

    public Matrix(double[,] input) : this(input.GetLength(0), input.GetLength(1))
    {
      var height = GetHeight();
      var width = GetWidth();

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          data[j, i] = input[j, i];
        }
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

    public Matrix(double[] input, int height, int width) : this(height, width)
    {
      CheckEqual(width * height, input.Length);
      for (int i = 0; i < width * height; i++)
      {
        Set(i, input[i]);
      }
    }

    public double Get(int row, int column)
    {
      Check(
        row < GetHeight() && column < GetWidth() &&
        row >= 0 && column >= 0);

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
      var rowAndColumn = computeRowAndColumn(idx);
      return this.data[rowAndColumn.Row, rowAndColumn.Column];
    }

    //Only allowed to be used in constructors
    private void Set(int idx, double value)
    {
      var rowAndColumn = computeRowAndColumn(idx);
      this.data[rowAndColumn.Row, rowAndColumn.Column] = value;
    }

    private RowAndColumnIndex computeRowAndColumn(int idx)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();
      return new RowAndColumnIndex(row, column);
    }

    public static Matrix generateRandomMatrix(int height, int width, int seed = 1)
    {
      //Does a double-allocation, which is inefficient, but the code is clean.
      var rnd = new Random(seed);
      return new Matrix(height, width).ElementWiseBasedOnSelf(x => 2 * rnd.NextDouble() - 1);
    }

    public static Matrix generateZeroMatrix(int height, int width)
    {
      return new Matrix(height, width);
    }

    public static Matrix generateOnesMatrix(int height, int width)
    {
      //Does a double-allocation, which is inefficient, but the code is clean.
      return new Matrix(height, width).ElementWiseBasedOnSelf(x => 1);
    }


    public Matrix times(double factor)
    {
      return ElementWiseBasedOnSelf(x => x * factor);
    }

    public Matrix plus(Matrix term)
    {
      return ElementWiseBasedOnOtherMatrix((x, y) => x + y, term);
    }

    public Matrix plus(double term)
    {
      return ElementWiseBasedOnSelf(x => x + term);
    }

    public Matrix mtimes(Matrix data2)
    {
      CheckEqual(this.GetWidth(), data2.GetHeight());
      var returnValue = new MutableMatrix(this.GetHeight(), data2.GetWidth());
      for (int i = 0; i < this.GetHeight(); i++)
      {
        for (int j = 0; j < data2.GetWidth(); j++)
        {
          for (int k = 0; k < this.GetWidth(); k++)
          {
            returnValue.Set(i, j, returnValue.Get(i, j) + this.Get(i, k) * data2.Get(k, j));
          }
        }
      }
      return returnValue;
    }

    public Matrix exp()
    {
      return ElementWiseBasedOnSelf(Math.Exp);
    }

    public Matrix elementPower(int v)
    {
      return this.ElementWiseBasedOnSelf(x => Math.Pow(x, v));
    }

    public Matrix elementMult(Matrix data2)
    {
      return ElementWiseBasedOnOtherMatrix((x, y) => x * y, data2);
    }

    public Matrix transpose()
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(width, height);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(i, j, Get(j, i));
        }
      }
      return rValue;
    }

    public Matrix abs()
    {
      return ElementWiseBasedOnSelf(Math.Abs);
    }

    public Matrix mean()
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(1, width);
      for (int i = 0; i < width; i++)
      {
        var sumValue = 0.0;
        for (int j = 0; j < height; j++)
        {
          sumValue += Get(j, i);
        }
        rValue.Set(0, i, sumValue / height);
      }
      return rValue;
    }

    private Matrix ElementWiseBasedOnSelf(Func<double, double> op)
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, op(Get(j, i)));
        }
      }
      return rValue;
    }

    private Matrix ElementWiseBasedOnOtherMatrix(Func<double, double, double> op, Matrix m)
    {
      var height = GetHeight();
      var width = GetWidth();

      CheckEqual(height, m.GetHeight());
      CheckEqual(width, m.GetWidth());


      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, op(Get(j, i), m.Get(j, i)));
        }
      }
      return rValue;
    }
  }
}

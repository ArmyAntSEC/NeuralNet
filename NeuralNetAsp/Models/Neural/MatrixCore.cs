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

    //Only allowed in constructors
    private void Set(int idx, double value)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();

      this.data[row, column] = value;
    }

    public static Matrix generateRandomMatrix(int height, int width, int seed = 1)
    {
      var rnd = new Random(seed);
      var rValue = new Matrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.data[j, i] = 2 * rnd.NextDouble() - 1;
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

    public Matrix plus(double term)
    {
      //TODO: Inefficient
      var adder = Matrix.generateOnesMatrix(GetHeight(), GetWidth()).times(term);
      return this.plus(adder);
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

    //TODO: Double check or delete this
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
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, Math.Exp(this.Get(j, i)));
        }
      }
      return rValue;

    }

    public Matrix elementPower(int v)
    {
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, Math.Pow(this.Get(j, i), v));
        }
      }
      return rValue;
    }

    public Matrix elementMult(Matrix data2)
    {
      var height = GetHeight();
      var width = GetWidth();
      CheckEqual(height, data2.GetHeight());
      CheckEqual(width, data2.GetWidth());

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, Get(j, i) * data2.Get(j, i));
        }
      }
      return rValue;
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
      var height = GetHeight();
      var width = GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, Math.Abs(Get(j, i)));
        }
      }
      return rValue;
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
      this.Data[row, column] = value;
    }

    public void Set(int idx, double value)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();

      this.Data[row, column] = value;
    }
  }
}

using System;

namespace NeuralNetAsp.Models.MatrixCore
{
  public static class MatrixScalarOperators
  {
    public static Matrix ElementWiseBasedOnSelf(Matrix m, Func<double, double> op)
    {
      var height = m.GetHeight();
      var width = m.GetWidth();

      var rValue = new MutableMatrix(height, width);
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, op(m.Get(j, i)));
        }
      }
      return rValue;
    }


    public static Matrix abs(this Matrix m)
    {
      return ElementWiseBasedOnSelf(m, Math.Abs);
    }

    public static Matrix exp(this Matrix m)
    {
      return ElementWiseBasedOnSelf(m, Math.Exp);
    }

    public static Matrix elementPower(this Matrix m, int v)
    {
      return ElementWiseBasedOnSelf(m, x => Math.Pow(x, v));
    }

    public static Matrix times(this Matrix m, double factor)
    {
      return ElementWiseBasedOnSelf(m, x => x * factor);
    }

    public static Matrix plus(this Matrix m, double term)
    {
      return ElementWiseBasedOnSelf(m, x => x + term);
    }
  }
}
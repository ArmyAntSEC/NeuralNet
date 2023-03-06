using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Models.MatrixCore
{
  public static class MAtrixElementwizeOperators
  {
    public static Matrix ElementWiseBasedOnOtherMatrix(Matrix m1, Func<double, double, double> op, Matrix m2)
    {
      var height = m2.GetHeight();
      var width = m2.GetWidth();

      CheckEqual(height, m2.GetHeight());
      CheckEqual(width, m2.GetWidth());
      var rValue = new MutableMatrix(height, width);

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue.Set(j, i, op(m1.Get(j, i), m2.Get(j, i)));
        }
      }
      return rValue;
    }
    public static Matrix plus(this Matrix m1, Matrix m2)
    {
      return ElementWiseBasedOnOtherMatrix(m1, (x, y) => x + y, m2);
    }

    public static Matrix elementMult(this Matrix m1, Matrix m2)
    {
      return ElementWiseBasedOnOtherMatrix(m1, (x, y) => x * y, m2);
    }
  }
}
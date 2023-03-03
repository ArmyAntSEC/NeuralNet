using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class MathCore
  {
    public static double DotProduct(double[] a, double[] b)
    {
      CheckEqual(a.Length, b.Length);
      var sum = 0.0;
      for (int i = 0; i < a.Length; i++)
      {
        sum += a[i] * b[i];
      }
      return sum;
    }

    public static double SigmoidFunction(double input)
    {
      return 1 / (1 + Math.Exp(-input));
    }
  }
}

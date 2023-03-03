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

    public static double[,] generateRandomMatrix(int height, int width)
    {
      var randomGenerator = Medallion.Rand.Create();
      var rValue = new double[height, width];
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          rValue[j, i] = Medallion.Rand.NextGaussian(randomGenerator);
        }
      }
      return rValue;
    }
  }
}

using System;
using System.Diagnostics.Contracts;

namespace NeuralNetAsp.Neural
{
  public class Perceptron
  {
    private double[] weights;
    private double bias;

    public Perceptron(double[] weights, double bias)
    {
      this.weights = weights;
      this.bias = bias;
    }

    public double FeedForward(double[] inputs)
    {
      return 0;
    }

    public static double DotProduct(double[] a, double[] b)
    {
      Contract.Requires(a.Length == b.Length);
      var sum = 0.0;
      for (int i = 0; i < a.Length; i++)
      {
        sum += a[i] * b[i];
      }
      return sum;
    }

    public static double ActivationFunction(double input)
    {
      return 1 / (1 + Math.Exp(-input));
    }

    public static double ActivationFunctionDerivative(double input)
    {
      return ActivationFunction(input) * (1 - ActivationFunction(input));
    }
  }
}
using System;
using System.Diagnostics.Contracts;

namespace NeuralNetAsp.Neural
{
  public class Perceptron
  {
    private double[] weights;
    private double offset;

    public Perceptron(double[] weights, double bias)
    {
      this.weights = weights;
      this.offset = bias;
    }

    public Perceptron(PerceptronDefinition def)
    {
      this.weights = def.Weights;
      this.offset = def.Offset;
    }


    public double FeedForward(double[] inputs)
    {
      var linearResult = DotProduct(inputs, weights) + offset;
      return ActivationFunction(linearResult);
    }

    public int GetNumberOfInputs()
    {
      return weights.Length;
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

  public class PerceptronDefinition
  {
    private readonly double[] weights;
    public double[] Weights
    {
      get { return weights; }
    }

    private readonly double offset;
    public double Offset
    {
      get { return offset; }
    }
    public PerceptronDefinition(double[] weights, double offset)
    {
      this.weights = weights;
      this.offset = offset;
    }
  }
}
using System;

namespace NeuralNetAsp.Neural
{
  public class Node
  {
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
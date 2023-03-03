using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class MathCore
  {
    public static double SigmoidFunction(double input)
    {
      return 1 / (1 + Math.Exp(-input));
    }
  }
}

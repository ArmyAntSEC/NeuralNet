using System;
using System.Diagnostics.Contracts;

namespace NeuralNetAsp.Neural
{
  public class InputLayer
  {
    private double[] values;

    public double[] GetOutput()
    {
      throw new NotImplementedException();
    }

    public void SetValues(double[] values)
    {
      this.values = values;
    }


  }
}
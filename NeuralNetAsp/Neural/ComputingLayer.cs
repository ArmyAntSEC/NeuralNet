using System.Collections.Generic;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class ComputingLayer : ILayerBase
  {

    private Perceptron[] perceptrons;

    public ComputingLayer(List<PerceptronDefinition> defs)
    {
      Check(defs.Count > 0);

      this.perceptrons = new Perceptron[defs.Count];
      this.perceptrons[0] = new Perceptron(defs[0]);

      for (var i = 1; i < perceptrons.Length; i++)
      {
        this.perceptrons[i] = new Perceptron(defs[i]);
        CheckEqual(this.perceptrons[i - 1].GetNumberOfInputs(), this.perceptrons[i].GetNumberOfInputs());
      }
    }

    public double[] GetOutput(double[] inputs)
    {
      CheckEqual(inputs.Length, perceptrons[0].GetNumberOfInputs());

      var output = new double[perceptrons.Length];
      for (int i = 0; i < perceptrons.Length; i++)
      {
        output[i] = this.perceptrons[i].FeedForward(inputs);
      }
      return output;
    }
  }
}
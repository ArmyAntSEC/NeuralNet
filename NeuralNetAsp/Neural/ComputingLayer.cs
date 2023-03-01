using System.Collections.Generic;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class ComputingLayer : ILayerBase
  {

    private double[] inputs;

    private Perceptron[] perceptrons;

    public ComputingLayer(List<double[]> weights, double[] offsets)
    {
      CheckEqual(weights.Count, offsets.Length);
      this.perceptrons = new Perceptron[offsets.Length];
      for (var i = 0; i < offsets.Length; i++)
      {
        this.perceptrons[i] = new Perceptron(weights[i], offsets[i]);
      }
    }

    public ComputingLayer(List<PerceptronDefinition> defs)
    {
      this.perceptrons = new Perceptron[defs.Count];
      for (var i = 0; i < perceptrons.Length; i++)
      {
        this.perceptrons[i] = new Perceptron(defs[i]);
      }
    }

    public double[] GetOutput()
    {
      var output = new double[perceptrons.Length];
      for (int i = 0; i < perceptrons.Length; i++)
      {
        output[i] = this.perceptrons[i].FeedForward(inputs);
      }
      return output;
    }

    public void SetInputs(double[] inputs)
    {
      CheckEqual(inputs.Length, perceptrons[0].GetNumberOfInputs());
      this.inputs = inputs;
    }
  }
}
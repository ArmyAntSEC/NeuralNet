using System.Collections.Generic;
using static NeuralNetAsp.Neural.Guard;

namespace NeuralNetAsp.Neural
{
  public class ComputingLayer : ILayerBase
  {
    private List<double[]> weights;
    private double[] offsets;

    private double[] inputs;

    private Perceptron[] perceptrons;

    public ComputingLayer(List<double[]> weights, double[] offsets)
    {
      CheckEqual(weights.Count, offsets.Length);
      this.weights = weights;
      this.offsets = offsets;
      this.perceptrons = new Perceptron[offsets.Length];
      for (var i = 0; i < offsets.Length; i++)
      {
        this.perceptrons[i] = new Perceptron(weights[i], offsets[i]);
      }
    }

    public double[] GetOutput()
    {
      var output = new double[offsets.Length];
      for (int i = 0; i < offsets.Length; i++)
      {
        output[i] = this.perceptrons[i].FeedForward(inputs);
      }
      return output;
    }

    public void SetInputs(double[] inputs)
    {
      CheckEqual(inputs.Length, weights[0].Length);
      this.inputs = inputs;
    }
  }
}
using System.Collections.Generic;
using static NeuralNetAsp.Neural.Guard;

namespace NeuralNetAsp.Neural
{
  public class ComputingLayer : ILayerBase
  {
    private List<double[]> weights;
    private double[] offsets;

    private double[] inputs;

    private Perceptron perceptrons;

    public ComputingLayer(List<double[]> weights, double[] offsets)
    {
      CheckEqual(weights[0].Length, offsets.Length);
      this.weights = weights;
      this.offsets = offsets;
      this.perceptrons = new Perceptron(weights[0], offsets[0]);
    }

    public double[] GetOutput()
    {
      return new double[] { this.perceptrons.FeedForward(inputs) };
    }

    public void SetInputs(double[] inputs)
    {
      CheckEqual(inputs.Length, offsets.Length);
      this.inputs = inputs;
    }
  }
}
using System.Collections.Generic;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class Network : ILayerBase
  {
    private ILayerBase[] layers;

    public Network(ILayerBase[] layers)
    {
      Check(layers != null);
      Check(layers.Length > 0);
      //TODO: Check that the input size matches the previous output size.
      this.layers = layers;
    }

    public double[] GetOutput(double[] inputs)
    {
      double[] tempStorage = inputs;
      foreach (var layer in layers)
      {
        tempStorage = layer.GetOutput(tempStorage);
      }
      return tempStorage;
    }
  }
}
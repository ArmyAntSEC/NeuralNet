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
      for (var i = 0; i < layers.Length - 1; i++)
      {
        //var thisLayerOutputSize = layers[i].
      }
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
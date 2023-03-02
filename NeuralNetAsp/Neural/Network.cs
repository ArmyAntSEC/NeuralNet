using System;
using System.Diagnostics;
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
      var layerOutputSize = layers[0].GetOutputSize();
      for (var i = 1; i < layers.Length; i++)
      {
        var inputLayerSize = layers[i].GetInputSize();
        CheckEqual(layerOutputSize, inputLayerSize);
        layerOutputSize = inputLayerSize;
      }
      this.layers = layers;
    }

    public double[] FeedForward(double[] inputs)
    {
      double[] tempStorage = inputs;
      foreach (var layer in layers)
      {
        tempStorage = layer.FeedForward(tempStorage);
      }
      return tempStorage;
    }

    public int GetInputSize()
    {
      throw new System.NotImplementedException();
    }

    public int GetOutputSize()
    {
      throw new System.NotImplementedException();
    }
  }
}
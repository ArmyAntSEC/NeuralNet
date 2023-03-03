using System;

namespace NeuralNetAsp.Neural
{
  public class NeuralNetCore
  {
  }

  public class NetworkParameters
  {
    public double[,] weightsLayerOne;
    public double[,] biasLayerOne;
    public double[,] weightsLayerTwo;
    public double[,] biasLayerTwo;

    public NetworkParameters(int inputSize, int hiddenSize, int outputSize)
    {

    }


  }
}
using System;

namespace NeuralNetAsp.Neural
{
  public class NeuralNetCore
  {
  }

  public class NetworkParameters
  {
    private double[,] weightsLayerOneParam;
    public double[,] weightsLayerOne
    {
      get { return weightsLayerOneParam; }
    }

    private double[,] biasLayerOneParam;
    public double[,] biasLayerOne
    {
      get { return biasLayerOneParam; }
    }

    private double[,] weightsLayerTwoParam;
    public double[,] weightsLayerTwo
    {
      get { return weightsLayerTwoParam; }
    }

    private double[,] biasLayerTwoParam;
    public double[,] biasLayerTwo
    {
      get { return biasLayerTwoParam; }
    }

    public NetworkParameters(int inputSize, int hiddenSize, int outputSize)
    {
      weightsLayerOneParam = MathCore.generateRandomMatrix(hiddenSize, inputSize);
      biasLayerOneParam = MathCore.generateZeroMatrix(hiddenSize, 1);
      weightsLayerTwoParam = MathCore.generateRandomMatrix(outputSize, hiddenSize);
      biasLayerTwoParam = MathCore.generateZeroMatrix(outputSize, 1);
    }


  }
}
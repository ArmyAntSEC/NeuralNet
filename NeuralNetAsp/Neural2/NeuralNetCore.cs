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
      weightsLayerOne = MathCore.generateRandomMatrix(hiddenSize, inputSize);
      biasLayerOne = MathCore.generateZeroMatrix(hiddenSize, 1);
      weightsLayerTwo = MathCore.generateRandomMatrix(outputSize, hiddenSize);
      biasLayerTwo = MathCore.generateZeroMatrix(outputSize, 1);
    }


  }
}
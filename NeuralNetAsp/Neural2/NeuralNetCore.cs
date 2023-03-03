using System;

namespace NeuralNetAsp.Neural
{
  public class NeuralNetCore
  {
    public static Matrix feedForward(Matrix input, NetworkParameters parameters)
    {
      return input;
    }
  }

  public class NetworkParameters
  {
    private Matrix weightsLayerOneParam;
    public Matrix weightsLayerOne
    {
      get { return weightsLayerOneParam; }
    }

    private Matrix biasLayerOneParam;
    public Matrix biasLayerOne
    {
      get { return biasLayerOneParam; }
    }

    private Matrix weightsLayerTwoParam;
    public Matrix weightsLayerTwo
    {
      get { return weightsLayerTwoParam; }
    }

    private Matrix biasLayerTwoParam;
    public Matrix biasLayerTwo
    {
      get { return biasLayerTwoParam; }
    }

    public NetworkParameters(int inputSize, int hiddenSize, int outputSize)
    {
      weightsLayerOneParam = Matrix.generateRandomMatrix(hiddenSize, inputSize);
      biasLayerOneParam = Matrix.generateZeroMatrix(hiddenSize, 1);
      weightsLayerTwoParam = Matrix.generateRandomMatrix(outputSize, hiddenSize);
      biasLayerTwoParam = Matrix.generateZeroMatrix(outputSize, 1);
    }

    public NetworkParameters(Matrix weightsLayerOne, Matrix biasLayerOne, Matrix weightsLayerTwo, Matrix biasLayerTwo)
    {
      this.weightsLayerOneParam = weightsLayerOne;
      this.biasLayerOneParam = biasLayerOne;
      this.weightsLayerTwoParam = weightsLayerTwo;
      this.biasLayerTwoParam = biasLayerTwo;
    }


  }
}
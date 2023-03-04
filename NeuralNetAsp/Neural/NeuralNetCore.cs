using System;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Neural
{
  public class NeuralNetCore
  {
    public static Matrix computeCrossEntropyCost(MutableMatrix answer, MutableMatrix expectedAnswer, NetworkParameters parameters)
    {
      CheckEqual(answer.GetWidth(), expectedAnswer.GetWidth());
      CheckEqual(answer.GetHeight(), expectedAnswer.GetHeight());

      return null;
    }

    public static Matrix feedForward(Matrix input, NetworkParameters parameters)
    {
      var stepOne = input.mtimes(parameters.weightsLayerOne).times(-1);
      var stepTwo = stepOne.exp().plus(1);
      var layerOne = stepTwo.elementPower(-1);
      return layerOne;
    }

    public static Matrix feedForwardSingleLayer(Matrix input, Matrix weights)
    {
      var stepOne = input.mtimes(weights).times(-1);
      var stepTwo = stepOne.exp().plus(1);
      return stepTwo.elementPower(-1);
    }
  }

  public class NetworkParameters
  {
    private Matrix weightsLayerOneParam;
    public Matrix weightsLayerOne
    {
      get { return weightsLayerOneParam; }
    }


    private Matrix weightsLayerTwoParam;
    public Matrix weightsLayerTwo
    {
      get { return weightsLayerTwoParam; }
    }

    public NetworkParameters(int inputSize, int hiddenSize, int outputSize)
    {
      weightsLayerOneParam = Matrix.generateRandomMatrix(hiddenSize, inputSize);
      weightsLayerTwoParam = Matrix.generateRandomMatrix(outputSize, hiddenSize);
    }

    public NetworkParameters(Matrix weightsLayerOne, Matrix weightsLayerTwo)
    {
      this.weightsLayerOneParam = weightsLayerOne;
      this.weightsLayerTwoParam = weightsLayerTwo;
    }


  }
}
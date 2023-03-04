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

    public static Matrix computeDelta(Matrix error, Matrix layer)
    {
      var layerExp = layer.exp();
      var inverseDenominator = layerExp.plus(1).elementPower(2).elementPower(-1);
      var numerator = error.elementMult(layerExp);
      return numerator.elementMult(inverseDenominator);
    }

    public static FeedForwardResult feedForward(Matrix input, NetworkParameters parameters)
    {
      var layerOne = feedForwardSingleLayer(input, parameters.weightsLayerOne);
      return new FeedForwardResult(layerOne, feedForwardSingleLayer(layerOne, parameters.weightsLayerTwo));
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

  public class FeedForwardResult
  {
    private Matrix layerOneInternal;
    public Matrix LayerOne
    {
      get { return layerOneInternal; }
    }

    private Matrix layerTwoInternal;
    public Matrix LayerTwo
    {
      get { return layerTwoInternal; }
    }

    public FeedForwardResult(Matrix layerOne, Matrix layerTwo)
    {
      this.layerOneInternal = layerOne;
      this.layerTwoInternal = layerTwo;
    }
  }
}
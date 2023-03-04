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
      var firstStepLinear = parameters.weightsLayerOne.dot(input).plus(parameters.biasLayerOne);
      var firstStep = firstStepLinear.tanh();
      var secondStepLinear = parameters.weightsLayerTwo.dot(firstStep).plus(parameters.biasLayerTwo);
      var secondStep = secondStepLinear.tanh();
      return secondStep;
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
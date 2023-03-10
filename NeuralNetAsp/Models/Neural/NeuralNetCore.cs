using System;
using static NeuralNetAsp.Utils.Guard;
using NeuralNetAsp.Models.MatrixCore;

namespace NeuralNetAsp.Models.Neural
{
  public class NeuralNetCore
  {

    public static Matrix computeDelta(Matrix error, Matrix layer)
    {
      var layerExp = layer.exp();
      var inverseDenominator = layerExp.plus(1).elementPower(2).elementPower(-1);
      var numerator = error.elementMult(layerExp);
      return numerator.elementMult(inverseDenominator);
    }

    public static Deltas computeDeltas(FeedForwardResult feedForwardResult, NetworkParameters parameters, Matrix expectedResponse)
    {
      var layerTwoError = feedForwardResult.LayerTwo.plus(expectedResponse.times(-1));
      var layerTwoDelta = computeDelta(layerTwoError, feedForwardResult.LayerTwo);

      var layerOneError = layerTwoDelta.mtimes(parameters.WeightsLayerTwo.transpose());
      var layerOneDelta = computeDelta(layerOneError, feedForwardResult.LayerOne);

      var errorVal = layerTwoError.abs().mean();
      CheckEqual(errorVal.GetHeight(), 1);
      CheckEqual(errorVal.GetWidth(), 1);

      return new Deltas(layerOneDelta, layerTwoDelta, errorVal.Get(0));
    }

    public static NetworkParameters computeNewWeights(NetworkParameters parameters, Matrix input, double alpha, FeedForwardResult feedForwardResult, Deltas deltas)
    {
      var newWeightsLayerOne = computenewWeightsSingleLayer(parameters.WeightsLayerOne,
        alpha, input, deltas.LayerOneDelta);
      var newWeightsLayerTwo = computenewWeightsSingleLayer(parameters.WeightsLayerTwo,
        alpha, feedForwardResult.LayerOne, deltas.LayerTwoDelta);

      return new NetworkParameters(newWeightsLayerOne, newWeightsLayerTwo);
    }

    public static Matrix computenewWeightsSingleLayer(Matrix weights, double alpha, Matrix layer, Matrix layerDelta)
    {
      var inner = layer.transpose().mtimes(layerDelta);
      var correction = inner.times(-alpha);
      return weights.plus(correction);
    }

    public static FeedForwardResult feedForward(Matrix input, NetworkParameters parameters)
    {
      var layerOne = feedForwardSingleLayer(input, parameters.WeightsLayerOne);
      return new FeedForwardResult(layerOne, feedForwardSingleLayer(layerOne, parameters.WeightsLayerTwo));
    }

    public static Matrix feedForwardSingleLayer(Matrix input, Matrix weights)
    {
      var stepOne = input.mtimes(weights).times(-1);
      var stepTwo = stepOne.exp().plus(1);
      return stepTwo.elementPower(-1);
    }

    public static NetworkParameters TrainNetwork(TrainingDataSetComplete param)
    {
      double error = Double.PositiveInfinity;
      var parameters = param.Parameters;
      for (int i = 0; i < param.NumberOfIterations && error > param.ErrorTolerance; i++)
      {
        var feedForwardResults = feedForward(param.TrainingDataInput, parameters);
        var deltas = computeDeltas(feedForwardResults, parameters, param.TrainingDataOutput);
        parameters = computeNewWeights(parameters, param.TrainingDataInput, param.Alpha, feedForwardResults, deltas);
        error = deltas.ErrorVal;
      }
      return parameters;
    }
  }

  public class NetworkParameters
  {
    public Matrix WeightsLayerOne { get; }
    public Matrix WeightsLayerTwo { get; }

    public NetworkParameters(int inputSize, int hiddenSize, int outputSize)
    {
      WeightsLayerOne = Matrix.generateRandomMatrix(hiddenSize, inputSize);
      WeightsLayerTwo = Matrix.generateRandomMatrix(outputSize, hiddenSize);
    }

    public NetworkParameters(Matrix weightsLayerOne, Matrix weightsLayerTwo)
    {
      WeightsLayerOne = weightsLayerOne;
      WeightsLayerTwo = weightsLayerTwo;
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

  public class Deltas
  {
    private Matrix layerOneDeltaInternal;
    public Matrix LayerOneDelta
    {
      get { return layerOneDeltaInternal; }
    }

    private Matrix layerTwoDeltaInternal;
    public Matrix LayerTwoDelta
    {
      get { return layerTwoDeltaInternal; }
    }

    private double errorValInternal;
    public double ErrorVal
    {
      get { return errorValInternal; }
    }

    public Deltas(Matrix layerOneDelta, Matrix layerTwoDelta, double errorVal)
    {
      this.layerOneDeltaInternal = layerOneDelta;
      this.layerTwoDeltaInternal = layerTwoDelta;
      this.errorValInternal = errorVal;
    }
  }

  public class TrainingDataSetComplete
  {
    public Matrix TrainingDataInput { get; }
    public Matrix TrainingDataOutput { get; }
    public double Alpha { get; }
    public int NumberOfIterations { get; }
    public double ErrorTolerance { get; }
    public NetworkParameters Parameters { get; }

    public TrainingDataSetComplete(
      Matrix trainingDataInput, Matrix trainingDataOutput, double alpha,
      int numberOfIterations, double errorTolerance, NetworkParameters parameters)
    {
      TrainingDataInput = trainingDataInput;
      TrainingDataOutput = trainingDataOutput;
      Alpha = alpha;
      NumberOfIterations = numberOfIterations;
      ErrorTolerance = errorTolerance;
      Parameters = parameters;
    }
  }
}
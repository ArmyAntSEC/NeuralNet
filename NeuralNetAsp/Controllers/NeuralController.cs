using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NeuralNetAsp.Models.MatrixCore;
using NeuralNetAsp.Models.Neural;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Controllers
{
  [Route("api/[controller]")]
  public class NeuralController : Controller
  {
    // GET api/neural/<id>
    [HttpGet]
    public JsonResult Get([FromBody] PredictData data)
    {
      int inputSize = 4;
      int hiddenLayerSize = data.LayerOneWeights.Length / inputSize;
      var weightsOne = new Matrix(data.LayerOneWeights, inputSize, hiddenLayerSize);
      var weightsTwo = new Matrix(data.LayerTwoWeights, hiddenLayerSize, 1);

      var parameters = new NetworkParameters(weightsOne, weightsTwo);

      var input = new Matrix(data.input, 1, inputSize);

      var feedForwardResult = NeuralNetCore.feedForward(input, parameters);
      return Json(feedForwardResult.LayerTwo.Get(0));
    }

    // POST api/neural
    [HttpPost]
    public JsonResult Post([FromBody] TrainingData input)
    {
      //TODO: Store the training variables in DynamoDB and just return an ID so the end user need not pass these back when doing a prediction.
      try
      {
        int inputSize = 4;
        int outputSize = 1;

        var trainingDataInput = new Matrix(input.Input, input.Output.Length, inputSize);
        var trainingDataOutput = new Matrix(input.Output);

        CheckEqual(trainingDataInput.GetWidth(), inputSize);
        CheckEqual(trainingDataOutput.GetWidth(), outputSize);
        CheckEqual(trainingDataInput.GetHeight(), trainingDataOutput.GetHeight());

        double alpha = 0.001;

        int numberOfIterations = 1000000;

        double errorTolerance = 0.05;

        var firstLayerWeights = new Matrix(new double[,] {
          {-0.1660, -0.7065, -0.2065},
          {0.4406, -0.8153, 0.0776},
          {-0.9998, -0.6275, -0.1616},
          {-0.3953, -0.3089, 0.3704}
        });

        var secondLayerWeights = new Matrix(new double[,] {
          {-0.5911},
          {0.7562},
          {-0.9452}
        });

        var parameters = new NetworkParameters(firstLayerWeights, secondLayerWeights);

        var param = new TrainingDataSetComplete(
          trainingDataInput, trainingDataOutput, alpha,
          numberOfIterations, errorTolerance, parameters);

        var finalParameters = NeuralNetCore.TrainNetwork(param);

        var response = new ResponseData(
          finalParameters.WeightsLayerOne.ToArray(),
          finalParameters.WeightsLayerTwo.ToArray());

        return Json(response);
      }
      catch (Exception e)
      {
        return Json(e.Message);
      }
    }
  }

  public class PredictData : ResponseData
  {
    public double[] input { get; set; }

    public PredictData(double[] input, double[] layerOne, double[] layerTwo) : base(layerOne, layerTwo)
    {
      this.input = input;
    }
  }

  public class ResponseData
  {
    [JsonPropertyName("layer_one_weights")]
    public double[] LayerOneWeights { get; set; }
    [JsonPropertyName("layer_two_weights")]
    public double[] LayerTwoWeights { get; set; }

    public ResponseData(double[] layerOne, double[] layerTwo)
    {
      LayerOneWeights = layerOne;
      LayerTwoWeights = layerTwo;
    }
  }

  public class TrainingData
  {
    public double[] Input { get; set; }
    public double[] Output { get; set; }
  }
}

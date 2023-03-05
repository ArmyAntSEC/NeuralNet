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
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "Value: " + id + "\n";
    }

    // POST api/neural
    [HttpPost]
    public JsonResult Post([FromBody] TrainingData input)
    {
      try
      {
        int inputSize = 4;
        int outputSize = 1;

        var trainingDataInput = new Matrix(input.input, input.output.Length, inputSize);
        var trainingDataOutput = new Matrix(input.output);

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
  public class ResponseData
  {
    public double[] layerOneWeights { get; set; }
    public double[] layerTwoWeights { get; set; }

    public ResponseData(double[] layerOne, double[] layerTwo)
    {
      layerOneWeights = layerOne;
      layerTwoWeights = layerTwo;
    }
  }

  public class TrainingData
  {
    public double[] input { get; set; }
    public double[] output { get; set; }
  }
}

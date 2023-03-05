using System;
using Microsoft.AspNetCore.Mvc;
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
    public String Post([FromBody] String input)
    {
      return "POST: " + input;
      /*
      try
      {
        int inputSize = 5;
        int outputSize = 1;

        var trainingDataInput = new Matrix(data.TrainingDataInput);
        var trainingDataOutput = new Matrix(data.TrainingDataOutput);
        CheckEqual(trainingDataInput.GetWidth(), inputSize);
        CheckEqual(trainingDataInput.GetWidth(), outputSize);
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

        return "Hello World!";
      }
      catch (Exception e)
      {
        return "Error: " + e.Message;
      }
      */
    }
  }
  public class TrainingData
  {
    [JsonPropertyName("training_data_input")]
    public double[,] TrainingDataInput { get; }
    [JsonPropertyName("training_data_output")]
    public double[] TrainingDataOutput { get; }
  }
}

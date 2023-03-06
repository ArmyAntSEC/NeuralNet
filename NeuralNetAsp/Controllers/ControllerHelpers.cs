using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NeuralNetAsp.Models.MatrixCore;
using NeuralNetAsp.Models.Neural;
using static NeuralNetAsp.Utils.Guard;

namespace NeuralNetAsp.Controllers
{

  public class PredictData
  {
    public double[] layerOneWeights { get; set; }
    public double[] layerTwoWeights { get; set; }

    public double[] input { get; set; }
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

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
    public double[] LayerOneWeights { get; set; }
    public double[] LayerTwoWeights { get; set; }

    public double[] Input { get; set; }
  }

  public class ResponseData
  {
    public double[] LayerOneWeights { get; set; }
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

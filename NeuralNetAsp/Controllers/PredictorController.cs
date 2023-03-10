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
  public class PredictorController : Controller
  {
    // POST api/predictor/
    [HttpPost]
    public JsonResult Get([FromBody] PredictData data)
    {
      int inputSize = 4;
      int hiddenLayerSize = data.LayerOneWeights.Length / inputSize;
      var weightsOne = new Matrix(data.LayerOneWeights, inputSize, hiddenLayerSize);
      var weightsTwo = new Matrix(data.LayerTwoWeights, hiddenLayerSize, 1);

      var parameters = new NetworkParameters(weightsOne, weightsTwo);

      var input = new Matrix(data.Input, 1, inputSize);

      var feedForwardResult = NeuralNetCore.feedForward(input, parameters);
      return Json(feedForwardResult.LayerTwo.Get(0));
    }
  }
}

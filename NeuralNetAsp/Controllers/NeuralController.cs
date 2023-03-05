using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

    // POST api/values
    [HttpPost]
    public String Post([FromBody] TrainingData data)
    {
      return "Hello World!";
    }
  }
  public class TrainingData
  {

  }
}

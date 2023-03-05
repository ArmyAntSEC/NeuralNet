using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NeuralNetAsp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : Controller
  {
    // GET api/values
    [HttpGet]
    public PlaceHolderResponse Get()
    {
      return new PlaceHolderResponse("World");
    }
  }

  public class PlaceHolderResponse
  {
    public string Hello { get; set; }

    public PlaceHolderResponse(string hello)
    {
      this.Hello = hello;
    }
  }
}

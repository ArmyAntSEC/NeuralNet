using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NeuralNetAsp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HeartbeatController : Controller
  {
    // GET api/values
    [HttpGet]
    public HeartBeatResponse Get()
    {
      return new HeartBeatResponse("Alive!");
    }
  }

  public class HeartBeatResponse
  {
    public string Status { get; set; }

    public HeartBeatResponse(string status)
    {
      this.Status = status;
    }
  }
}

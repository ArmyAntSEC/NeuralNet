using System;
using Microsoft.AspNetCore.Mvc;

namespace NeuralNetAsp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HeartbeatController : Controller
  {
    [HttpGet]
    public HeartBeatResponse Get()
    {
      return new HeartBeatResponse("Alive!");
    }

    [HttpPost]
    public HeartBeatResponse Post([FromBody] String value)
    {
      return new HeartBeatResponse("Posted: " + value);
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

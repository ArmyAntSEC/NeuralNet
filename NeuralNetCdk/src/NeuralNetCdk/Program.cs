using Amazon.CDK;

namespace NeuralNetCdk
{
  sealed class Program
  {
    public static void Main(string[] args)
    {
      var app = new App();
      new NeuralNetCdkStack(app, "NeuralNetCdkStack", new StackProps
      {
        Env = new Environment
        {
          Region = "eu-west-1"
        }
      });
      app.Synth();
    }
  }
}

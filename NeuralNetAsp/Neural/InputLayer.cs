namespace NeuralNetAsp.Neural
{
  public class InputLayer : ILayerBase
  {
    private double[] values;

    public double[] GetOutput()
    {
      return this.values;
    }

    public void SetInputs(double[] inputs)
    {
      this.values = inputs;
    }
  }
}
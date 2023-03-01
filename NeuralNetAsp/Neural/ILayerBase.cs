namespace NeuralNetAsp.Neural
{
  public interface ILayerBase
  {
    public double[] GetOutput();
    public void SetInputs(double[] inputs);
  }
}
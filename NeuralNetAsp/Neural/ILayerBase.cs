namespace NeuralNetAsp.Neural
{
  public interface ILayerBase
  {
    public double[] FeedForward(double[] inputs);

    public int GetOutputSize();

    public int GetInputSize();
  }
}
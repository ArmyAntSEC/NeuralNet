namespace NeuralNetAsp.Models.MatrixCore
{
  public class MutableMatrix : Matrix
  {
    public MutableMatrix(int height, int width) : base(height, width)
    {
    }

    public MutableMatrix(Matrix matrix) : base(matrix)
    {

    }

    public void Set(int row, int column, double value)
    {
      this.Data[row, column] = value;
    }

    public void Set(int idx, double value)
    {
      var column = idx / GetHeight();
      var row = idx - column * GetHeight();

      this.Data[row, column] = value;
    }
  }
}
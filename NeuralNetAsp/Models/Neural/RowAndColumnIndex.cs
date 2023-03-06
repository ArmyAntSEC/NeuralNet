namespace NeuralNetAsp.Models.MatrixCore
{
  public class RowAndColumnIndex
  {
    public int Row { get; set; }
    public int Column { get; set; }

    public RowAndColumnIndex(int row, int column)
    {
      Row = row;
      Column = column;
    }
  }
}
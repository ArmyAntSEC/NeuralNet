namespace NeuralNetAsp.Utils
{
  sealed class Guard
  {
    public static void Check(bool condition)
    {
      if (!condition)
      {
        throw new System.ArgumentException("Condition failed");
      }
    }
    public static void CheckEqual(object one, object two)
    {
      if (!one.Equals(two))
      {
        throw new System.ArgumentException("Expected " + one + " to equal " + two);
      }
    }
  }
}
namespace NeuralNetAsp.Neural
{
  class Guard
  {
    public static void Check(bool condition)
    {
      if (!condition)
      {
        throw new System.Exception("Condition failed");
      }
    }
    public static void CheckEqual(object one, object two)
    {
      if (!one.Equals(two))
      {
        throw new System.Exception("Expected " + one + " to equal " + two);
      }
    }
  }
}
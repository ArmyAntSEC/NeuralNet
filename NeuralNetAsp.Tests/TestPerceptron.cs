using NeuralNetAsp.Neural;
namespace NeuralNetAsp.Tests;


public class UnitTest1
{
  [Fact]
  public void Test1()
  {
    var sut = new Perceptron();

    Assert.Equal(0, sut.weight);

  }
}
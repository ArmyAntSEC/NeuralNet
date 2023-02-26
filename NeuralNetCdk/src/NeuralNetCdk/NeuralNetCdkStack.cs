using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;

namespace NeuralNetCdk
{
  public class NeuralNetCdkStack : Stack
  {
    internal NeuralNetCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
      // Create DynamoDB Table
      new Table(this, "WeightParameterStorage", new TableProps
      {
        PartitionKey = new Attribute { Name = "PK", Type = AttributeType.STRING },
        SortKey = new Attribute { Name = "SK", Type = AttributeType.STRING },
        BillingMode = BillingMode.PAY_PER_REQUEST
      });
    }
  }
}

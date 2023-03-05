using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;

using System.IO;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Amazon.CDK.AWS.Ecr.Assets;

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

      //Create a load balanced webserver
      new ApplicationLoadBalancedFargateService(this, "NeuralNetApiServer",
        new ApplicationLoadBalancedFargateServiceProps
        {
          TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
          {
            Image = ContainerImage.FromDockerImageAsset(
              new DockerImageAsset(this, "NeuralNetAsp", new DockerImageAssetProps
              {
                Directory = Path.Combine(Directory.GetCurrentDirectory(), "..", "NeuralNetAsp")

              })
            )
          },
          PublicLoadBalancer = true
        }
      );
    }
  }
}

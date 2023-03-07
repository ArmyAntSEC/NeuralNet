# NeuralNET demo

This is a small demo of a pair of endpoints that train and predict 
respectively with a simple neural network.

The code is written mosly as a style demo of C# and illustrates the following best practices:
- The entire core is written using Test Driven Development, the only method except Pair Programming to be proven to raise quality.
- Single-interaction deployment, both to a local system and to public deployment. Delays between a developer finishes writing code and that code can be in the hands of users causes a slowdown in the development speed and a lowering of the quality, as shown by Googles DORA research.
- Automatic provisioning of infrastructure, meaning there is never a disconnect between what the code expects and what the infrastructure looks like. 

## How to use
Assuming you have dotnet 7.0.103 installed, docker running and the AWS tools configured, then you should be able to go into the scripts folder and run either of test_and_deploy_local.sh or test_and_deploy_cdk.sh to build and test. The first one stops after doing a sanity check in local docker. The second one then provisions resources and deploys to Amazon.

As of writing, those containers are running and you can run the following two commands:
### Train

This command sends some training data and responds with the weight parameters for the resulting neural net.
```
curl -X POST http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com:80/api/neural/ -H 'Content-Type: application/json' -d '{"input":[-0.33,-0.33,-0.33,-0.33,-0.33,0.69,0.94,0.5,0.75,0.67,0,1,1,0,1,1,0,0,1,1],"output":[1,1,0,1,0]}'
```

### Predict
This command sends the weight parameters from above and an input and does a prediction. The data below should give a value close to 1.

```
curl -X GET http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com:80/api/neural/ -H 'Content-Type: application/json' -d '{"layerOneWeights":[0.20501795434531928,4.851008781645798,-5.094595716697624,-3.173856984876353,-0.3819715353811023,5.729700533135146,-7.097541432816082,-3.6949973939179026,-0.1169280643114341,-3.895180491131896,2.054754866046295,1.697961953959847],"layerTwoWeights":[5.44573619851985,5.522509842250541,-4.927694841147059],"input":[-0.33,0.69,0,1]}'
```

## Future improvements
The code is rather rought and there are some major improvements that this code would need:

- Use the provisioned DynamoDb database to store the trained parameters so 
they do not need to be sent in the GET stage (Partially done in the database branch).
- Give the ASP.NET controller code better structure and error handling.
- Convert the build scripts into a proper CI system.
- Add complete tests for all standard error cases for all matrix operations and properly error on all of them.

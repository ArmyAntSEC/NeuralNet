#Run the below in the NeuralNetCdk directory
#dotnet build #In what directory?
pwd
cd ../NeuralNetCdk

pwd
cdk bootstrap
cdk deploy --require-approval never

sleep 10

echo "Heartbeat:"
curl --fail http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com/api/heartbeat/ || exit 1

echo "Training:"
curl -X POST http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com:80/api/neural/ -H 'Content-Type: application/json' -d '{"input":[-0.33,-0.33,-0.33,-0.33,-0.33,0.69,0.94,0.5,0.75,0.67,0,1,1,0,1,1,0,0,1,1],"output":[1,1,0,1,0]}'
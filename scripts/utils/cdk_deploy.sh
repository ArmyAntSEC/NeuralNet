#Run the below in the NeuralNetCdk directory
#dotnet build #In what directory?
pwd
cd ../NeuralNetCdk

pwd
cdk bootstrap
cdk deploy --require-approval never

sleep 10

curl --fail http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com/api/heartbeat/ || exit 1
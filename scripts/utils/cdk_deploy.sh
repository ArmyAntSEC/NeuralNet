cd ../NeuralNetCdk

echo "*** Bootstrapping CDK. Only actually needed once"
cdk bootstrap

echo "*** DEploying with CDK"
cdk deploy --require-approval never

echo "*** Waiting 20 seconds for system to come online fully.!"
sleep 20

echo "*** Doing API tests on deployed system"
NEURAL_NET_BASE_URL=http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com:80 dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.ApiTests/NeuralNetAsp.ApiTests.csproj || exit

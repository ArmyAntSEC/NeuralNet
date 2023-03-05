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

echo "Predict"
curl -X GET http://neura-neura-jjeraxh3bz3p-691205240.eu-west-1.elb.amazonaws.com:80/api/neural/ -H 'Content-Type: application/json' -d '{"layerOneWeights":[0.20501795434531928,4.851008781645798,-5.094595716697624,-3.173856984876353,-0.3819715353811023,5.729700533135146,-7.097541432816082,-3.6949973939179026,-0.1169280643114341,-3.895180491131896,2.054754866046295,1.697961953959847],"layerTwoWeights":[5.44573619851985,5.522509842250541,-4.927694841147059],"input":[-0.33,0.69,0,1]}'
echo "The above should be close to 1.0"
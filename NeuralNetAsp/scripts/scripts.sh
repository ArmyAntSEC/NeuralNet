# Run unit tests
echo "*** Starting unit tests" 
dotnet test ../NeuralNetAsp.Tests/NeuralNetAsp.Tests.csproj || exit

# Build and deploy a container locally
docker stop  neuralnetasp
docker rm neuralnetasp
docker build -t neuralnetasp .  
docker images 
docker run -d -p 80:80 --name neuralnetasp neuralnetasp
echo "*** Waiting for docker to start" && sleep 10

# Test that the APIs work
curl --fail http://localhost:80/api/values/ || exit

# Deploy to AWS

# Test that AWS works
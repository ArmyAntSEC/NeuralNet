# Run unit tests
echo "*** Starting unit tests" 
dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.Tests/NeuralNetAsp.Tests.csproj || exit

# Build and deploy a container locally
echo "*** Building container"
sh ./utils/docker_build.sh

echo "*** Giving Docker 10s to start"
sleep 10

echo "*** Doing API tests"
NEURAL_NET_BASE_URL=http://localhost:80 dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.ApiTests/NeuralNetAsp.ApiTests.csproj || exit

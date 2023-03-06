# Run unit tests
echo "*** Starting unit tests" 
dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.Tests/NeuralNetAsp.Tests.csproj || exit

# Build and deploy a container locally
sh ./utils/docker_build.sh

dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.ApiTests/NeuralNetAsp.ApiTests.csproj || exit

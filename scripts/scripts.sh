# Run unit tests
echo "*** Starting unit tests" 
dotnet test ../NeuralNetAsp.Tests/NeuralNetAsp.Tests.csproj || exit

# Build and deploy a container locally
sh docker_build.sh

# Test that the APIs work
curl --fail http://localhost:80/api/values/ || exit

# Deploy to AWS

# Test that AWS works
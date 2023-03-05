# Run unit tests
echo "*** Starting unit tests" 
dotnet test -l "console;verbosity=detailed" ../NeuralNetAsp.Tests/NeuralNetAsp.Tests.csproj || exit

# Build and deploy a container locally
sh ./utils/docker_build.sh

# Test that the APIs work
echo "Heartbeat:"
curl --fail http://localhost:80/api/heartbeat/ || exit 1
echo

echo "GET"
curl --fail http://localhost:80/api/neural/45 || exit 1
echo

echo "POST"
curl -X POST http://localhost:80/api/neural/ -H 'Content-Type: application/json' -d '{}' || exit 1
echo
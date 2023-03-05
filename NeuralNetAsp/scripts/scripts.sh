docker stop  neuralnetasp
docker rm neuralnetasp
docker build -t neuralnetasp .  
docker images 
docker run -d -p 80:80 --name neuralnetasp neuralnetasp

sleep 10

curl http://localhost:80/api/values/
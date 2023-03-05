docker stop  neuralnetasp
docker rm neuralnetasp
docker build -t neuralnetasp .  
docker images 
docker run -d -p 80:80 --name neuralnetasp neuralnetasp

until [ "`docker inspect -f {{.State.Running}} neuralnetasp`"=="true" ]; do
    sleep 0.1;
done;

curl http://localhost:80/api/values/
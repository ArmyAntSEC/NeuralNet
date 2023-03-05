#!/bin/sh
cd ../NeuralNetAsp
pwd
docker stop  neuralnetasp
docker rm neuralnetasp
docker build -t neuralnetasp .  
docker images 
docker run -d -p 80:80 --name neuralnetasp neuralnetasp
echo "*** Waiting for docker to start" && sleep 10
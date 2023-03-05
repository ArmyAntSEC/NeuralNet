# NeuralNET demo

This is a small dem of a pair of endpoints that train and predict 
respectively with a simple neural network.

The code is written mosly as a style demo of Test Driven Devlopment in 
C# with ASP.NET and as a POC for using AWS CBK in C# to provision 
infrastructure and deploy automatically. 

There are three major improvements that this code would need:

- Use the provisioned DynamoDb database to store the trained parameters so 
they do not need to be sent in the GET stage (Partially done in the database branch).
- Give the ASP.NET controller code better structure and error handling.
- Convert the build scripts into a proper CI system including using the 
test framework to also test the final APIs in end-to-end testing.

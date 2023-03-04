%%
clc
clear

%%set non-random seed
rng('default');
rng(1);

%% input data
filename = 'data.txt';
delimiterIn = ',';
Data = importdata(filename,delimiterIn);
Data = Data(1:5,1:5);

%% create training and testing matrices
[entries, attributes] = size(Data);
entries_breakpoint = round(entries*.90); %set breakpoint for training and testing data at 90% of dataset
inputlayersize=attributes-1;

outputlayersize=1;
trainingdata = Data(1:entries_breakpoint,:); %truncate first 90% entries for training data
trainingdata_inputs = trainingdata(:,1:inputlayersize); %90%x9 matrix input training data
trainingdata_outputs = trainingdata(:,inputlayersize+1:end); %90:1 matrix output training data
testingdata = Data(entries_breakpoint:end,:); %truncate last 10 entries for testing data
testingdata_inputs= testingdata(:,1:inputlayersize); %10:9 matrix input testing data
testingdata_outputs= testingdata(:,inputlayersize+1:end); %10:1 matrix output testing data

error_tolerance = 0.05;
hiddenlayersize=3;

%% initialize random synapse weights with a mean of 0
syn0 = 2*rand(inputlayersize,hiddenlayersize) - 1; %random matrix, inputlayersize X hiddenlayersize
syn1 = 2*rand(hiddenlayersize,outputlayersize) - 1; %random matrix, hiddenlayersize X outputlayersize

%% feedforward training and testing data
layer0=trainingdata_inputs;
layer1=(1)./(1+exp(-1.*(layer0*syn0)));
layer2=(1)./(1+exp(-1.*(layer1*syn1)));
err = sqrt(sum((layer2-trainingdata_outputs).^2));
fprintf("Untrained: Mean Squared Error with Trainingdata: %f\n", err)

layer0=testingdata_inputs;
layer1=(1)./(1+exp(-1.*(layer0*syn0)));
layer2=(1)./(1+exp(-1.*(layer1*syn1)));
err = sqrt(sum((layer2-testingdata_outputs).^2));
fprintf("Untrained: Mean Squared Error with Testingdata: %f\n", err)

%% Do iteration
alpha=0.001;
fprintf("Training with alpha: %f\n", alpha)

for iter=1:1000000
    %feedforward
    layer0=trainingdata_inputs;
    layer1=(1)./(1+exp(-1.*(layer0*syn0))); %multiply inputs by weights and apply sigmoid activation functoin
    layer2=(1)./(1+exp(-1.*(layer1*syn1))); %multiply hidden layer by 2nd set of weights and apply sigmoid activation function

    %cost function (how much did we miss)
    layer2_error=layer2-trainingdata_outputs;

    %which direction is the target value
    layer2_delta = layer2_error.*(exp(layer2)./(exp(layer2)+1).^2);

    %how much did each l1 value contribute to l2 error
    layer1_error = layer2_delta*syn1.';

    %which direction is target l1
    layer1_delta = layer1_error.*(exp(layer1)./(exp(layer1)+1).^2);

    %adjust values
    errorval = mean(abs(layer2_error));
    syn1 = syn1 - alpha.*(layer1.'*layer2_delta);
    syn0 = syn0 - alpha.*(layer0.'*layer1_delta);

    if errorval<error_tolerance
        fprintf("Stopping at: %f error\n", errorval)
        break
    end

    %print out debug data
    if iter==1 || mod(iter,10000) == 0
        fprintf("\titer=%.0f, Error: %f\n", iter, errorval)
        %syn0
        %syn1
    end
end
if errorval>error_tolerance
    fprintf("Value Below Tolerance not found, please adjust alpha\n\n")
else
    fprintf("Value Below Tolerance found: %f\n\n", errorval)
end


%% feedforward training data
layer0=trainingdata_inputs;
layer1=(1)./(1+exp(-1.*(layer0*syn0))); 
layer2=(1)./(1+exp(-1.*(layer1*syn1))); 
err = sqrt(sum((layer2-trainingdata_outputs).^2));
fprintf("Trained: Mean Squared Error with Trainingdata: %f\n", err)

%% feedforward testing data
layer0=testingdata_inputs;
layer1=(1)./(1+exp(-1.*(layer0*syn0))); 
layer2=(1)./(1+exp(-1.*(layer1*syn1))); 
err = sqrt(sum((layer2-testingdata_outputs).^2));
fprintf("Trained: Mean Squared Error with Testingdata: %f\n", err)


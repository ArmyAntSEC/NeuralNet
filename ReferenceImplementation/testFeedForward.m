clear;
syn0 = [0.1 0.2; 0.3 0.4];
layer0=[1 2; 3 4; 5 6];
layer1=(1)./(1+exp(-1.*(layer0*syn0)));

stepOne = -1*layer0*syn0;
stepTwo = exp(stepOne)+1;
stepThree = 1./(stepTwo)

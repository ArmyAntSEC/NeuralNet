clear;
syn1 = [1 2; 3 4];
alpha = 0.001;
layer1 = [1 2; 3 4; 5 6];
layer2_delta = [1 2; 3 4; 5 6];

syn1 = syn1 - alpha.*(layer1.'*layer2_delta);

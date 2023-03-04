clear;
layer2_error=[1 2; 3 4];
layer2 = [1 2; 3 4];
layer2_delta = layer2_error.*exp(layer2)./(exp(layer2)+1).^2
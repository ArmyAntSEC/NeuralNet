#!/bin/sh
# First test and deploy locally 
echo "Testing locallt and deploying to local server"
sh test_and_deploy_local.sh

# Deploy with cdk
sh ./utils/cdk_deploy.sh

# Finally test that the CDK deployment works
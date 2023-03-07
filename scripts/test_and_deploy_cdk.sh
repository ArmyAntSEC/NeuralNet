#!/bin/sh
# First test and deploy locally 
echo "*** Testing locally and deploying to local server"
sh test_and_deploy_local.sh

# Deploy with cdk and do sanity check
echo "*** Deploying with CDK"
sh ./utils/cdk_deploy.sh
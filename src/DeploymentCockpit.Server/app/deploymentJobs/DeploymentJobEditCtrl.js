"use strict";

app.controller("DeploymentJobEditCtrl", function ($scope, $routeParams, deploymentJobsSvc) {
    $scope.projectID = $routeParams.projectID;
    $scope.deploymentJob = deploymentJobsSvc.get($routeParams.deploymentJobID);
});

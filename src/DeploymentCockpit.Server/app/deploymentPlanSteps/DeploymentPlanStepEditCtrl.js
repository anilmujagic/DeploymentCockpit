"use strict";

app.controller("DeploymentPlanStepEditCtrl", function ($scope, $routeParams, deploymentPlanStepsSvc) {
    $scope.projectID = $routeParams.projectID;
    $scope.deploymentPlanStepID = $routeParams.deploymentPlanStepID;
    $scope.deploymentPlanStep = deploymentPlanStepsSvc.get($routeParams.deploymentPlanStepID);
});

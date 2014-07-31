"use strict";

app.controller("DeploymentPlanEditCtrl", function ($scope, $routeParams, deploymentPlansSvc) {
    $scope.deploymentPlan = deploymentPlansSvc.get($routeParams.deploymentPlanID);
});

"use strict";

app.controller("DeploymentPlanDetailsCtrl", function ($scope, $routeParams, $location, deploymentPlansSvc) {
    $scope.deploymentPlanID = $routeParams.deploymentPlanID;  // To be available for nested directives even before main object is loaded.
    $scope.deploymentPlan = deploymentPlansSvc.get($routeParams.deploymentPlanID);
});

"use strict";

app.controller("DeploymentPlanStepCreateCtrl", function ($scope, $routeParams) {
    $scope.projectID = $routeParams.projectID;
    $scope.deploymentPlanStep = { deploymentPlanID: $routeParams.deploymentPlanID };
});

"use strict";

app.controller("DeploymentPlanCreateCtrl", function ($scope, $routeParams) {
    $scope.deploymentPlan = { projectID: $routeParams.projectID };
});

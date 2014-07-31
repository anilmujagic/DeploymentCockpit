"use strict";

app.controller("DeploymentJobCreateCtrl", function ($scope, $routeParams) {
    $scope.projectID = $routeParams.projectID;
    $scope.deploymentJob = { projectID: $routeParams.projectID };
});

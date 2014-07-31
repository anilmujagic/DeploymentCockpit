"use strict";

app.controller("ProjectDetailsCtrl", function ($scope, $routeParams, $location, projectsSvc) {
    $scope.projectID = $routeParams.projectID;  // To be available for nested directives even before project is loaded.
    $scope.project = projectsSvc.get($routeParams.projectID);
});

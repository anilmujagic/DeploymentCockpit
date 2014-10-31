"use strict";

app.controller("VariablesHierarchyCtrl", function ($scope, $routeParams, projectsSvc) {
    $scope.projectID = $routeParams.projectID;  // To be available for nested directives even before project is loaded.
    $scope.project = projectsSvc.get($routeParams.projectID);

    $scope.variablesHierarchy = projectsSvc.getAll({
        projectID: $routeParams.projectID,
        variablesHierarchy: true
    });

    $scope.isLoading = function () {
        return !$scope.variablesHierarchy.$resolved;
    };
});

"use strict";

app.controller("DeploymentJobDetailsCtrl", function ($scope, $routeParams, $modal, $interval, $timeout,
    $anchorScroll, $location, deploymentJobsSvc, deploymentJobStepsSvc) {

    $scope.deploymentJobID = $routeParams.deploymentJobID;  // To be available for nested directives even before main object is loaded.

    $scope.reloadData = function () {
        $scope.deploymentJob = deploymentJobsSvc.get($scope.deploymentJobID);
        deploymentJobStepsSvc.getAll({ deploymentJobID: $scope.deploymentJobID }).$promise.then(function (data) {
            $scope.deploymentJobSteps = data;
            $timeout(function () {
                scrollToBottom();
            });
        });
    };
    $scope.reloadData();

    $scope.isLoading = function () {
        return !$scope.deploymentJob.$resolved || !$scope.deploymentJobSteps.$resolved;
    };

    var refreshInterval = $interval(function () {
        if (!$scope.deploymentJob
            || $scope.deploymentJob.statusKey === "Queued"
            || $scope.deploymentJob.statusKey === "Running") {
            $scope.reloadData();
        } else {
            $interval.cancel(refreshInterval);
        }
    }, 5000);

    $scope.$on("$destroy", function () {
        if (refreshInterval) {
            $interval.cancel(refreshInterval);
        }
    });

    $scope.showExecutionDetails = function (executionReference) {
        $scope.executionDetails = deploymentJobStepsSvc.get(executionReference);

        $scope.modalInstance = $modal.open({
            templateUrl: "app/deploymentJobs/deploymentJobStepDetails.html",
            scope: $scope,
            size: 'lg'
        });
    };

    function scrollToBottom() {
        if ($scope.deploymentJob.statusKey === "Running") {
            $location.hash("page-bottom");
            $anchorScroll();
        }
    }
});

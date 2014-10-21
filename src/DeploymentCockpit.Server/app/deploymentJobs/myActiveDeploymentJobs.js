"use strict";

app.directive("myActiveDeploymentJobs", function () {
    return {
        templateUrl: "app/deploymentJobs/myActiveDeploymentJobs.html",
        scope: {},
        controller: function ($scope, $interval, deploymentJobsSvc) {
            $scope.reloadData = function () {
                $scope.deploymentJobs = deploymentJobsSvc.getAll({ allActive: true });
            };

            $scope.reloadData();

            $scope.isLoading = function () {
                return !$scope.deploymentJobs.$resolved;
            };

            var refreshInterval = $interval(function () {
                $scope.reloadData();
            }, 5000);

            $scope.$on("$destroy", function () {
                if (refreshInterval) {
                    $interval.cancel(refreshInterval);
                }
            });
        }
    };
});

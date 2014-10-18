"use strict";

app.directive("myDeploymentJobs", function () {
    return {
        templateUrl: "app/deploymentJobs/myDeploymentJobs.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, deploymentJobsSvc, notificationSvc) {
            var reloadData = function () {
                $scope.deploymentJobs = deploymentJobsSvc.getAll({ projectID: $scope.projectID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.deploymentJobs.$resolved;
            };
        }
    };
});

"use strict";

app.directive("myDeploymentPlans", function () {
    return {
        templateUrl: "app/deploymentPlans/myDeploymentPlans.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, deploymentPlansSvc, notificationSvc) {
            var reloadData = function () {
                $scope.deploymentPlans = deploymentPlansSvc.getAll({ projectID: $scope.projectID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.deploymentPlans.$resolved;
            };

            $scope.delete = function (deploymentPlan) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                deploymentPlansSvc.delete(deploymentPlan.deploymentPlanID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(deploymentPlan.name + " deployment plan is deleted.");
                            reloadData();
                        },
                        function () {
                            notificationSvc.error("Deployment plan deletion failed.");
                        }
                    );
            };
        }
    };
});

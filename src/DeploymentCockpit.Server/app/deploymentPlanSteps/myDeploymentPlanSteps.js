"use strict";

app.directive("myDeploymentPlanSteps", function () {
    return {
        templateUrl: "app/deploymentPlanSteps/myDeploymentPlanSteps.html",
        scope: {
            projectID: "=projectId",
            deploymentPlanID: "=deploymentPlanId"
        },
        controller: function ($scope, deploymentPlanStepsSvc, notificationSvc) {
            var reloadData = function () {
                $scope.deploymentPlanSteps = deploymentPlanStepsSvc.getAll({ deploymentPlanID: $scope.deploymentPlanID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.deploymentPlanSteps.$resolved;
            };

            $scope.delete = function (deploymentPlanStep) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                deploymentPlanStepsSvc.delete(deploymentPlanStep.deploymentPlanStepID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(deploymentPlanStep.name + " deployment step is deleted.");
                            reloadData();
                        }
                    );
            };
        }
    };
});

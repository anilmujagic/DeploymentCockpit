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
                if (!notificationSvc.confirmDelete(deploymentPlanStep.name)) {
                    return;
                }
                deploymentPlanStepsSvc.delete(deploymentPlanStep.deploymentPlanStepID)
                    .$promise.then(
                        function () {
                            notificationSvc.deleted(deploymentPlanStep.name);
                            reloadData();
                        }
                    );
            };
        }
    };
});

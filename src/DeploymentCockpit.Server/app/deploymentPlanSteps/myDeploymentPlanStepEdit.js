"use strict";

app.directive("myDeploymentPlanStepEdit", function () {
    return {
        templateUrl: "app/deploymentPlanSteps/myDeploymentPlanStepEdit.html",
        scope: {
            projectID: "=projectId",
            deploymentPlanStep: "="
        },
        controller: function ($scope, $location, deploymentPlanStepsSvc, notificationSvc, scriptsSvc, targetGroupsSvc) {
            $scope.scripts = scriptsSvc.getAll({ projectID: $scope.projectID });
            $scope.targetGroups = targetGroupsSvc.getAll({ projectID: $scope.projectID });

            $scope.onTargetGroupChanged = function () {
                if ($scope.deploymentPlanStep.targetGroupID) {
                    $scope.deploymentPlanStep.allTargetGroups = false;
                }
            };
            $scope.onAllTargetGroupsChange = function () {
                if ($scope.deploymentPlanStep.allTargetGroups) {
                    $scope.deploymentPlanStep.targetGroupID = null;
                }
            };

            $scope.save = function (deploymentPlanStep) {
                deploymentPlanStepsSvc.save(deploymentPlanStep, deploymentPlanStep.deploymentPlanStepID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(deploymentPlanStep.name + " deployment step environment is saved.");
                            if (deploymentPlanStep.deploymentPlanStepID) {
                                $location.url("DeploymentPlan/Details/" + response.deploymentPlanID);
                            } else {
                                $location.url("DeploymentPlanStep/Edit/" + response.deploymentPlanStepID
                                    + "?projectID=" + $scope.projectID);
                            }
                        }
                    );
            };
        }
    };
});

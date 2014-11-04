"use strict";

app.directive("myDeploymentJobEdit", function () {
    return {
        templateUrl: "app/deploymentJobs/myDeploymentJobEdit.html",
        scope: {
            projectID: "=projectId",
            deploymentJob: "="
        },
        controller: function ($scope, $location, deploymentJobsSvc, notificationSvc,
            deploymentPlansSvc, projectEnvironmentsSvc, deploymentPlanParametersSvc) {
            $scope.deploymentPlans = deploymentPlansSvc.getAll({ projectID: $scope.projectID });
            $scope.projectEnvironments = projectEnvironmentsSvc.getAll({ projectID: $scope.projectID });
            $scope.parameters = [];

            $scope.isExistingJob = function () {
                if ($scope.deploymentJob.deploymentJobID) {
                    return true;
                } else {
                    return false;
                }
            }
            $scope.isJobRunning = function () {
                return $scope.deploymentJob.statusKey === "Running";
            }

            $scope.onPlanChange = function (deploymentPlanID) {
                $scope.parameters = deploymentPlanParametersSvc.getAll({ deploymentPlanID: deploymentPlanID });
            }

            $scope.save = function (deploymentJob, parameters) {
                deploymentJob.variables = [];
                if (parameters && parameters.length) {
                    for (var i = 0; i < parameters.length; i++) {
                        if (parameters[i].value) {
                            deploymentJob.variables.push({
                                name: parameters[i].name,
                                value: parameters[i].value
                            });
                        }
                    }
                }

                deploymentJobsSvc.save(deploymentJob, deploymentJob.deploymentJobID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.saved("Deployment job");
                            $location.url("DeploymentJob/Details/" + response.deploymentJobID);
                        }
                    );
            };
        }
    };
});

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
                deploymentJob.parameters = [];
                if (parameters && parameters.length) {
                    parameters.forEach(function (parameter) {
                        if (parameter.value) {
                            deploymentJob.parameters.push({
                                name: parameter.name,
                                value: parameter.value
                            });
                        }
                    });
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

"use strict";

app.directive("myDeploymentJobEdit", function () {
    return {
        templateUrl: "/app/deploymentJobs/myDeploymentJobEdit.html",
        scope: {
            projectID: "=projectId",
            deploymentJob: "="
        },
        controller: function ($scope, $location, deploymentJobsSvc, notificationSvc, deploymentPlansSvc, projectEnvironmentsSvc) {
            $scope.deploymentPlans = deploymentPlansSvc.getAll({ projectID: $scope.projectID });
            $scope.projectEnvironments = projectEnvironmentsSvc.getAll({ projectID: $scope.projectID });

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

            $scope.save = function (deploymentJob) {
                deploymentJobsSvc.save(deploymentJob, deploymentJob.deploymentJobID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success("Deployment job is saved.");
                            $location.url("DeploymentJob/Details/" + response.deploymentJobID);
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Deployment job save failed.");
                        }
                    );
            };
        }
    };
});

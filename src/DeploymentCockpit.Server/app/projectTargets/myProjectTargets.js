"use strict";

app.directive("myProjectTargets", function () {
    return {
        templateUrl: "app/projectTargets/myProjectTargets.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, $modal, projectTargetsSvc, notificationSvc,
            targetGroupsSvc, projectEnvironmentsSvc, targetsSvc) {
            var reloadData = function () {
                $scope.projectTargets = projectTargetsSvc.getAll({ projectID: $scope.projectID });

                // Dropdown lists
                $scope.targetGroups = targetGroupsSvc.getAll({ projectID: $scope.projectID });
                $scope.projectEnvironments = projectEnvironmentsSvc.getAll({ projectID: $scope.projectID });
                $scope.targets = targetsSvc.getAll();
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.projectTargets.$resolved;
            };

            $scope.add = function () {
                $scope.projectTarget = {};

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/projectTargets/projectTargetAdd.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (projectTarget) {
                if (!projectTarget) {
                    projectTarget = {
                        targetGroupID: null,
                        projectEnvironmentID: null,
                        targetID: null
                    };
                }

                projectTargetsSvc.save(projectTarget)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success("Project target is added.");
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (projectTarget) {
                if (!confirm("Do you really want to remove this?")) {
                    return;
                }
                projectTargetsSvc.delete(projectTarget.projectTargetID)
                    .$promise.then(
                        function () {
                            notificationSvc.success("Project target is removed.");
                            reloadData();
                        }
                    );
            };

            $scope.targetGroupEnvironmentDetails = function (targetGroupEnvironmentID) {
                $scope.targetGroupEnvironmentID = targetGroupEnvironmentID;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/targetGroupEnvironments/targetGroupEnvironmentDetails.html",
                    scope: $scope,
                    size: 'lg'
                });
            };

            $scope.projectTargetDetails = function (projectTargetID) {
                $scope.projectTargetID = projectTargetID;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/projectTargets/projectTargetDetails.html",
                    scope: $scope,
                    size: 'lg'
                });
            }
        }
    };
});

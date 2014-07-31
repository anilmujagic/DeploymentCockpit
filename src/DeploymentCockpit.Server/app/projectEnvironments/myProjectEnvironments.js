"use strict";

app.directive("myProjectEnvironments", function () {
    return {
        templateUrl: "/app/projectEnvironments/myProjectEnvironments.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, $modal, projectEnvironmentsSvc, notificationSvc) {
            var reloadData = function () {
                $scope.projectEnvironments = projectEnvironmentsSvc.getAll({ projectID: $scope.projectID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.projectEnvironments.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ projectID: $scope.projectID });
            };

            $scope.edit = function (projectEnvironment) {
                $scope.projectEnvironment = projectEnvironment;

                $scope.modalInstance = $modal.open({
                    templateUrl: "/app/projectEnvironments/projectEnvironmentEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (projectEnvironment) {
                projectEnvironmentsSvc.save(projectEnvironment, projectEnvironment.projectEnvironmentID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(projectEnvironment.name + " project environment is saved.");
                            $scope.modalInstance.close();
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Project environment save failed.");
                        }
                    );
            };

            $scope.delete = function (projectEnvironment) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                projectEnvironmentsSvc.delete(projectEnvironment.projectEnvironmentID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(projectEnvironment.name + " project environment is deleted.");
                            reloadData();
                        },
                        function () {
                            notificationSvc.error("Project environment deletion failed.");
                        }
                    );
            };
        }
    };
});

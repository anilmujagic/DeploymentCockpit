"use strict";

app.directive("myDeploymentJobParameters", function () {
    return {
        templateUrl: "app/deploymentJobParameters/myDeploymentJobParameters.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, $modal, deploymentJobParametersSvc, notificationSvc) {
            var reloadData = function () {
                $scope.parameters = deploymentJobParametersSvc.getAll({ projectID: $scope.projectID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.parameters.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ projectID: $scope.projectID });
            };

            $scope.edit = function (parameter) {
                $scope.parameter = parameter;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/deploymentJobParameters/deploymentJobParameterEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (parameter) {
                deploymentJobParametersSvc.save(parameter, parameter.deploymentJobParameterID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.saved(parameter.name);
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (parameter) {
                if (!notificationSvc.confirmDelete(parameter.name)) {
                    return;
                }
                deploymentJobParametersSvc.delete(parameter.deploymentJobParameterID)
                    .$promise.then(
                        function () {
                            notificationSvc.deleted(parameter.name);
                            reloadData();
                        }
                    );
            };
        }
    };
});

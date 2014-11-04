"use strict";

app.directive("myDeploymentPlanParameters", function () {
    return {
        templateUrl: "app/deploymentPlanParameters/myDeploymentPlanParameters.html",
        scope: {
            deploymentPlanID: "=planId"
        },
        controller: function ($scope, $modal, deploymentPlanParametersSvc, notificationSvc) {
            var reloadData = function () {
                $scope.parameters = deploymentPlanParametersSvc.getAll({ deploymentPlanID: $scope.deploymentPlanID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.parameters.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ deploymentPlanID: $scope.deploymentPlanID });
            };

            $scope.edit = function (parameter) {
                $scope.parameter = parameter;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/deploymentPlanParameters/deploymentPlanParameterEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (parameter) {
                deploymentPlanParametersSvc.save(parameter, parameter.deploymentPlanParameterID)
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
                deploymentPlanParametersSvc.delete(parameter.deploymentPlanParameterID)
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

"use strict";

app.directive("myVariables", function () {
    return {
        templateUrl: "app/variables/myVariables.html",
        scope: {
            scopeKey: "=scopeKey",
            scopeID: "=scopeId"
        },
        controller: function ($scope, $modal, variablesSvc, notificationSvc) {
            var reloadData = function () {
                if ($scope.scopeKey && $scope.scopeID) {
                    $scope.variables = variablesSvc.getAll({
                        scopeKey: $scope.scopeKey,
                        scopeID: $scope.scopeID
                    });
                }
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.variables.$resolved;
            };

            $scope.create = function () {
                $scope.edit({
                    scopeKey: $scope.scopeKey,
                    scopeID: $scope.scopeID
                });
            };

            $scope.edit = function (variable) {
                $scope.variable = variable;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/variables/variableEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (variable) {
                variablesSvc.save(variable, variable.variableID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(variable.name + " variable is saved.");
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (variable) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                variablesSvc.delete(variable.variableID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(variable.name + " variable is deleted.");
                            reloadData();
                        }
                    );
            };
        }
    };
});

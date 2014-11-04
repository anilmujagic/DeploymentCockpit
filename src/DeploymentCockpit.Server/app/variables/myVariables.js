"use strict";

app.directive("myVariables", function () {
    return {
        templateUrl: "app/variables/myVariables.html",
        scope: {
            scopeKey: "=",
            scopeID: "=scopeId",
            readOnly: "=",
            hideIfEmpty: "="
        },
        controller: function ($scope, $modal, variablesSvc, notificationSvc) {
            var reloadData = function () {
                if ($scope.scopeKey === "Global" || ($scope.scopeKey && $scope.scopeID)) {
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

            $scope.shouldHide = function () {
                return $scope.hideIfEmpty && (!$scope.variables || !$scope.variables.length)
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
                            notificationSvc.saved(variable.name);
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (variable) {
                if (!notificationSvc.confirmDelete(variable.name)) {
                    return;
                }
                variablesSvc.delete(variable.variableID)
                    .$promise.then(
                        function () {
                            notificationSvc.deleted(variable.name);
                            reloadData();
                        }
                    );
            };
        }
    };
});

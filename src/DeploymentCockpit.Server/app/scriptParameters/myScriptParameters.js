"use strict";

app.directive("myScriptParameters", function () {
    return {
        templateUrl: "app/scriptParameters/myScriptParameters.html",
        scope: {
            scriptID: "=scriptId"
        },
        controller: function ($scope, $modal, scriptParametersSvc, notificationSvc) {
            var reloadData = function () {
                if ($scope.scriptID) {
                    $scope.scriptParameters = scriptParametersSvc.getAll({ scriptID: $scope.scriptID });
                }
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.scriptParameters.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ scriptID: $scope.scriptID, isMandatory: true });
            };

            $scope.edit = function (scriptParameter) {
                $scope.scriptParameter = scriptParameter;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/scriptParameters/scriptParameterEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (scriptParameter) {
                scriptParametersSvc.save(scriptParameter, scriptParameter.scriptParameterID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.saved(scriptParameter.name);
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (scriptParameter) {
                if (!notificationSvc.confirmDelete(scriptParameter.name)) {
                    return;
                }
                scriptParametersSvc.delete(scriptParameter.scriptParameterID)
                    .$promise.then(
                        function () {
                            notificationSvc.deleted(scriptParameter.name);
                            reloadData();
                        }
                    );
            };
        }
    };
});

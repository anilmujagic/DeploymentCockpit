"use strict";

app.directive("myScriptParameters", function () {
    return {
        templateUrl: "/app/scriptParameters/myScriptParameters.html",
        scope: {
            scriptID: "=scriptId"
        },
        controller: function ($scope, $modal, scriptParametersSvc, notificationSvc) {
            var reloadData = function () {
                $scope.scriptParameters = scriptParametersSvc.getAll({ scriptID: $scope.scriptID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.scriptParameters.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ scriptID: $scope.scriptID });
            };

            $scope.edit = function (scriptParameter) {
                $scope.scriptParameter = scriptParameter;

                $scope.modalInstance = $modal.open({
                    templateUrl: "/app/scriptParameters/scriptParameterEdit.html",
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
                            notificationSvc.success(scriptParameter.name + " script parameter is saved.");
                            $scope.modalInstance.close();
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Script parameter save failed.");
                        }
                    );
            };

            $scope.delete = function (scriptParameter) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                scriptParametersSvc.delete(scriptParameter.scriptParameterID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(scriptParameter.name + " script parameter is deleted.");
                            reloadData();
                        },
                        function () {
                            notificationSvc.error("Script parameter deletion failed.");
                        }
                    );
            };
        }
    };
});

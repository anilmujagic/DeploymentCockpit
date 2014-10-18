"use strict";

app.controller("ScriptsCtrl", function ($scope, scriptsSvc, notificationSvc) {
    var reloadData = function () {
        $scope.scripts = scriptsSvc.getAll();
    };

    reloadData();

    $scope.isLoading = function () {
        return !$scope.scripts.$resolved;
    };

    $scope.delete = function (script) {
        if (!notificationSvc.confirmDelete(script.name)) {
            return;
        }
        scriptsSvc.delete(script.scriptID)
            .$promise.then(
                function () {
                    notificationSvc.deleted(script.name);
                    reloadData();
                }
            );
    };
});

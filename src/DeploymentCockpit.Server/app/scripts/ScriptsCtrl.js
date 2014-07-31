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
        if (!confirm("Do you really want to delete this?")) {
            return;
        }
        scriptsSvc.delete(script.scriptID)
            .$promise.then(
                function () {
                    notificationSvc.success(script.name + " script is deleted.");
                    reloadData();
                },
                function () {
                    notificationSvc.error("Script deletion failed.");
                }
            );
    };
});

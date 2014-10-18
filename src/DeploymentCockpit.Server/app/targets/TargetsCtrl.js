"use strict";

app.controller("TargetsCtrl", function ($scope, $modal, targetsSvc, credentialsSvc, notificationSvc, utilitySvc) {
    var reloadData = function () {
        $scope.targets = targetsSvc.getAll();
        $scope.credentials = credentialsSvc.getAll();
    };

    reloadData();

    $scope.isLoading = function () {
        return !$scope.targets.$resolved;
    };

    $scope.generateNewTargetKey = function (target) {
        target.targetKey = utilitySvc.generateEncryptionKey();
    };

    $scope.revertToDefaultPortNumber = function (target) {
        target.portNumber = utilitySvc.getDefaultPortNumber();
    };

    $scope.create = function () {
        $scope.edit({
            portNumber: utilitySvc.getDefaultPortNumber(),
            targetKey: utilitySvc.generateEncryptionKey()
        });
    };

    $scope.edit = function (target) {
        $scope.target = target;

        $scope.modalInstance = $modal.open({
            templateUrl: "app/targets/targetEdit.html",
            scope: $scope
        });

        $scope.modalInstance.result.finally(function () {
            reloadData();
        });
    };

    $scope.save = function (target) {
        targetsSvc.save(target, target.targetID)
            .$promise.then(
                function (response) {
                    notificationSvc.saved(target.name);
                    $scope.modalInstance.close();
                }
            );
    };

    $scope.delete = function (target) {
        if (!notificationSvc.confirmDelete(target.name)) {
            return;
        }
        targetsSvc.delete(target.targetID)
            .$promise.then(
                function () {
                    notificationSvc.deleted(target.name);
                    reloadData();
                }
            );
    };
});

"use strict";

app.controller("CredentialsCtrl", function ($scope, $modal, credentialsSvc, notificationSvc) {
    var reloadData = function () {
        $scope.credentials = credentialsSvc.getAll();
    };

    reloadData();

    $scope.isLoading = function () {
        return !$scope.credentials.$resolved;
    };

    $scope.create = function () {
        $scope.edit({});
    };

    $scope.edit = function (credential) {
        $scope.credential = credential;

        $scope.modalInstance = $modal.open({
            templateUrl: "app/credentials/credentialEdit.html",
            scope: $scope
        });

        $scope.modalInstance.result.finally(function () {
            reloadData();
        });
    };

    $scope.isExistingCredential = function () {
        if ($scope.credential.credentialID) {
            return true;
        }
        return false;
    };

    $scope.passwordsMatch = function () {
        var password = $scope.credential.password;
        var password2 = $scope.credential.password2;
        if (password || password2) {
            return password === password2;
        }
        return true;
    };

    $scope.save = function (credential) {
        if (!$scope.passwordsMatch()) {
            notificationSvc.error("Passwords don't match.");
            return;
        }

        credentialsSvc.save(credential, credential.credentialID)
            .$promise.then(
                function (response) {
                    notificationSvc.success(credential.name + " credential is saved.");
                    $scope.modalInstance.close();
                }
            );
    };

    $scope.delete = function (credential) {
        if (!confirm("Do you really want to delete this?")) {
            return;
        }
        credentialsSvc.delete(credential.credentialID)
            .$promise.then(
                function () {
                    notificationSvc.success(credential.name + " credential is deleted.");
                    reloadData();
                }
            );
    };
});

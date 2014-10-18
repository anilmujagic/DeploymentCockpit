"use strict";

app.controller("ProjectsCtrl", function ($scope, $modal, projectsSvc, notificationSvc) {
    var reloadData = function () {
        $scope.projects = projectsSvc.getAll();
    };

    reloadData();

    $scope.isLoading = function () {
        return !$scope.projects.$resolved;
    };

    $scope.create = function () {
        $scope.edit({});
    };

    $scope.edit = function (project) {
        $scope.project = project;

        $scope.modalInstance = $modal.open({
            templateUrl: "app/projects/projectEdit.html",
            scope: $scope
        });

        $scope.modalInstance.result.finally(function () {
            reloadData();
        });
    };

    $scope.save = function (project) {
        projectsSvc.save(project, project.projectID)
            .$promise.then(
                function (response) {
                    notificationSvc.saved(project.name);
                    $scope.modalInstance.close();
                }
            );
    };

    $scope.delete = function (project) {
        if (!notificationSvc.confirmDelete(project.name)) {
            return;
        }
        var dangerMessage = "!!! DANGER !!!\n\n" +
            "When project is deleted, all its related data is gone forever.\n" +
            "Are you completely sure that you want to delete this project?\n\n" +
            "Think before you click!\n\n";
        if (!confirm(dangerMessage)) {
            return;
        }
        projectsSvc.delete(project.projectID)
            .$promise.then(
                function () {
                    notificationSvc.deleted(project.name);
                    reloadData();
                }
            );
    };
});

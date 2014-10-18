"use strict";

app.directive("myUsers", function () {
    return {
        templateUrl: "app/users/myUsers.html",
        scope: {},
        controller: function ($scope, $modal, usersSvc, notificationSvc) {
            var reloadData = function () {
                $scope.users = usersSvc.getAll();
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.users.$resolved;
            };

            $scope.create = function () {
                $scope.edit({});
            };

            $scope.edit = function (user) {
                $scope.user = user;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/users/userEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (user) {
                usersSvc.save(user, user.userID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(user.name + " user is saved.");
                            $scope.modalInstance.close();
                        }
                    );
            };

            $scope.delete = function (user) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                usersSvc.delete(user.userID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(user.name + " user is deleted.");
                            reloadData();
                        }
                    );
            };
        }
    };
});

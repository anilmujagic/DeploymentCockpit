"use strict";

app.directive("myTargetGroups", function () {
    return {
        templateUrl: "app/targetGroups/myTargetGroups.html",
        scope: {
            projectID: "=projectId"
        },
        controller: function ($scope, $modal, targetGroupsSvc, notificationSvc) {
            var reloadData = function () {
                $scope.targetGroups = targetGroupsSvc.getAll({ projectID: $scope.projectID });
            };

            reloadData();

            $scope.isLoading = function () {
                return !$scope.targetGroups.$resolved;
            };

            $scope.create = function () {
                $scope.edit({ projectID: $scope.projectID });
            };

            $scope.edit = function (targetGroup) {
                $scope.targetGroup = targetGroup;

                $scope.modalInstance = $modal.open({
                    templateUrl: "app/targetGroups/targetGroupEdit.html",
                    scope: $scope
                });

                $scope.modalInstance.result.finally(function () {
                    reloadData();
                });
            };

            $scope.save = function (targetGroup) {
                targetGroupsSvc.save(targetGroup, targetGroup.targetGroupID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(targetGroup.name + " target group is saved.");
                            $scope.modalInstance.close();
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Target group save failed.");
                        }
                    );
            };

            $scope.delete = function (targetGroup) {
                if (!confirm("Do you really want to delete this?")) {
                    return;
                }
                targetGroupsSvc.delete(targetGroup.targetGroupID)
                    .$promise.then(
                        function () {
                            notificationSvc.success(targetGroup.name + " target group is deleted.");
                            reloadData();
                        },
                        function () {
                            notificationSvc.error("Target group deletion failed.");
                        }
                    );
            };
        }
    };
});

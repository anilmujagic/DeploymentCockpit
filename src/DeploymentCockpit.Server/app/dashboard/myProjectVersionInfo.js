"use strict";

app.directive("myProjectVersionInfo", function () {
    return {
        templateUrl: "app/dashboard/myProjectVersionInfo.html",
        scope: {},
        controller: function ($scope, $interval, dashboardDataSvc) {
            $scope.reloadData = function () {
                $scope.data = dashboardDataSvc.getProjectVersionInfo();
            };

            $scope.reloadData();

            $scope.isLoading = function () {
                return !$scope.data.$resolved;
            };
        }
    };
});

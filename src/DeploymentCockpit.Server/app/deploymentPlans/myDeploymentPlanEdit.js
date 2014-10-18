"use strict";

app.directive("myDeploymentPlanEdit", function () {
    return {
        templateUrl: "app/deploymentPlans/myDeploymentPlanEdit.html",
        scope: {
            deploymentPlan: "="
        },
        controller: function ($scope, $location, deploymentPlansSvc, notificationSvc) {
            $scope.save = function (deploymentPlan) {
                deploymentPlansSvc.save(deploymentPlan, deploymentPlan.deploymentPlanID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.saved(deploymentPlan.name);
                            $location.url("DeploymentPlan/Details/" + response.deploymentPlanID);
                        }
                    );
            };
        }
    };
});

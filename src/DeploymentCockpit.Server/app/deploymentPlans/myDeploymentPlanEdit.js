"use strict";

app.directive("myDeploymentPlanEdit", function () {
    return {
        templateUrl: "/app/deploymentPlans/myDeploymentPlanEdit.html",
        scope: {
            deploymentPlan: "="
        },
        controller: function ($scope, $location, deploymentPlansSvc, notificationSvc) {
            $scope.save = function (deploymentPlan) {
                deploymentPlansSvc.save(deploymentPlan, deploymentPlan.deploymentPlanID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(deploymentPlan.name + " deployment plan is saved.");
                            $location.url("DeploymentPlan/Details/" + response.deploymentPlanID);
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Deployment plan save failed.");
                        }
                    );
            };
        }
    };
});

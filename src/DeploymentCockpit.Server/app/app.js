"use strict";

var app = angular.module("app", ["ngRoute", "ngResource", "ui.bootstrap"]);

app.config(function ($routeProvider, $locationProvider, $httpProvider) {
    $routeProvider
        .when("/Dashboard", { controller: "DashboardCtrl", templateUrl: "app/dashboard/dashboard.html" })

        .when("/Settings", { controller: "SettingsCtrl", templateUrl: "app/settings/settings.html" })

        .when("/Projects", { controller: "ProjectsCtrl", templateUrl: "app/projects/projects.html" })
        .when("/Project/Details/:projectID", { controller: "ProjectDetailsCtrl", templateUrl: "app/projects/projectDetails.html" })

        .when("/Credentials", { controller: "CredentialsCtrl", templateUrl: "app/credentials/credentials.html" })

        .when("/Targets", { controller: "TargetsCtrl", templateUrl: "app/targets/targets.html" })

        .when("/Scripts", { controller: "ScriptsCtrl", templateUrl: "app/scripts/scripts.html" })
        .when("/Script/Create", { controller: "ScriptCreateCtrl", templateUrl: "app/scripts/scriptCreate.html" })
        .when("/Script/Edit/:scriptID", { controller: "ScriptEditCtrl", templateUrl: "app/scripts/scriptEdit.html" })

        .when("/DeploymentPlan/Create/:projectID", { controller: "DeploymentPlanCreateCtrl", templateUrl: "app/deploymentPlans/deploymentPlanCreate.html" })
        .when("/DeploymentPlan/Edit/:deploymentPlanID", { controller: "DeploymentPlanEditCtrl", templateUrl: "app/deploymentPlans/deploymentPlanEdit.html" })
        .when("/DeploymentPlan/Details/:deploymentPlanID", { controller: "DeploymentPlanDetailsCtrl", templateUrl: "app/deploymentPlans/deploymentPlanDetails.html" })

        .when("/DeploymentPlanStep/Create/:deploymentPlanID", { controller: "DeploymentPlanStepCreateCtrl", templateUrl: "app/deploymentPlanSteps/deploymentPlanStepCreate.html" })
        .when("/DeploymentPlanStep/Edit/:deploymentPlanStepID", { controller: "DeploymentPlanStepEditCtrl", templateUrl: "app/deploymentPlanSteps/deploymentPlanStepEdit.html" })

        .when("/DeploymentJob/Create/:projectID", { controller: "DeploymentJobCreateCtrl", templateUrl: "app/deploymentJobs/deploymentJobCreate.html" })
        .when("/DeploymentJob/Edit/:deploymentJobID", { controller: "DeploymentJobEditCtrl", templateUrl: "app/deploymentJobs/deploymentJobEdit.html" })
        .when("/DeploymentJob/Details/:deploymentJobID", { controller: "DeploymentJobDetailsCtrl", templateUrl: "app/deploymentJobs/deploymentJobDetails.html" })

        .otherwise({ redirectTo: "/Dashboard" });

    //$locationProvider.html5Mode(true);

    $httpProvider.interceptors.push("httpInterceptor");
});

app.run(function ($rootScope) {
    //$rootScope.dummy = ...
});

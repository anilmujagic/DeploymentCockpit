"use strict";

app.factory("deploymentPlanStepsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentPlanSteps");
});

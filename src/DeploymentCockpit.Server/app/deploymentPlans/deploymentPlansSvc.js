"use strict";

app.factory("deploymentPlansSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentPlans");
});

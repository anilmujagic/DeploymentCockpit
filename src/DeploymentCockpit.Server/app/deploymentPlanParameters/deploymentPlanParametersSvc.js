"use strict";

app.factory("deploymentPlanParametersSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentPlanParameters");
});

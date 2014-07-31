"use strict";

app.factory("deploymentJobStepsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentJobSteps");
});

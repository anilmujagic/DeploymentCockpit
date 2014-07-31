"use strict";

app.factory("projectEnvironmentsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "projectEnvironments");
});

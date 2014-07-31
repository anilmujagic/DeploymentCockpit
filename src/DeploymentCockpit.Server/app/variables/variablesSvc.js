"use strict";

app.factory("variablesSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "variables");
});

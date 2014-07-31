"use strict";

app.factory("projectsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "projects");
});

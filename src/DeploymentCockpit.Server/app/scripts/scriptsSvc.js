"use strict";

app.factory("scriptsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "scripts");
});

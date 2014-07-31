"use strict";

app.factory("credentialsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "credentials");
});

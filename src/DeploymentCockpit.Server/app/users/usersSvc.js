"use strict";

app.factory("usersSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "users");
});

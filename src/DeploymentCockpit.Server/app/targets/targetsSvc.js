"use strict";

app.factory("targetsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "targets");
});

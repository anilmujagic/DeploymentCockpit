"use strict";

app.factory("targetGroupsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "targetGroups");
});

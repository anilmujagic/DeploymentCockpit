"use strict";

app.factory("projectTargetsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "projectTargets");
});

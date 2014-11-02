"use strict";

app.factory("deploymentJobParametersSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentJobParameters");
});

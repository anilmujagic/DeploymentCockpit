"use strict";

app.factory("scriptParametersSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "scriptParameters");
});

"use strict";

app.factory("deploymentJobsSvc", function ($resource) {
    return deploymentCockpit.crudServiceFactory.create($resource, "deploymentJobs");
});

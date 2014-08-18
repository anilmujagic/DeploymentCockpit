"use strict";

app.factory("scriptExecutionSvc", function ($resource) {
    return {
        execute: function (descriptor) {
            return $resource("api/ScriptJobs").save(descriptor);
        }
    };
});

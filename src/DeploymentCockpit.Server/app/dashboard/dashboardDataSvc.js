"use strict";

app.factory("dashboardDataSvc", function ($resource) {
    return {
        getProjectVersionInfo: function () {
            return $resource("api/Dashboard/GetProjectVersionInfo").query();
        }
    };
});

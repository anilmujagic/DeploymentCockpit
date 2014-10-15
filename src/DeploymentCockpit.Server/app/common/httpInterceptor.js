"use strict";

app.factory("httpInterceptor", function ($q, notificationSvc) {
    return {
        "responseError": function (response) {
            if (response.data) {
                if (response.data instanceof Array) {
                    notificationSvc.errors(response.data);
                } else if (response.data.message) {
                    notificationSvc.error(response.data.message);
                } else {
                    notificationSvc.error(response.statusText);
                }
            } else {
                notificationSvc.error(response.statusText);
            }

            return $q.reject(response);
        }
    };
});

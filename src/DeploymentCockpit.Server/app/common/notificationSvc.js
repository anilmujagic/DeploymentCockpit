"use strict";

app.factory("notificationSvc", function () {
    return {
        success: function (message) {
            console.log("[Success] " + message);
            toastr.success(message);
        },
        info: function (message) {
            console.log("[Info] " + message);
            toastr.info(message);
        },
        warning: function (message) {
            console.log("[Warning] " + message);
            toastr.warning(message);
        },
        error: function (message) {
            console.log("[Error] " + message);
            toastr.error(message);
        },
        errors: function (errors) {
            for (var i = 0; i < errors.length; i++) {
                this.error(errors[i]);
            }
        }
    };
});

"use strict";

app.factory("utilitySvc", function () {
    return {

        getDefaultPortNumber: function () {
            return 29180;
        },

        generateEncryptionKey: function (keyLength) {
            var keyLength = keyLength || 50;
            var key = '';
            var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

            for (var i = 0; i < keyLength; i++) {
                key += chars.charAt(Math.floor(Math.random() * chars.length));
            }

            return key;
        }
    };
});

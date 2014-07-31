"use strict";

app.controller("ScriptEditCtrl", function ($scope, $routeParams, scriptsSvc) {
    $scope.scriptID = $routeParams.scriptID;  // For nested directives
    $scope.script = scriptsSvc.get($routeParams.scriptID);
});

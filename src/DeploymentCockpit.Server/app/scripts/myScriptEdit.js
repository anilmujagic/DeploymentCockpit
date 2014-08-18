"use strict";

app.directive("myScriptEdit", function () {
    return {
        templateUrl: "app/scripts/myScriptEdit.html",
        scope: {
            scriptID: "=scriptId",
            script: "="
        },
        controller: function ($scope, $location, scriptsSvc, notificationSvc, projectsSvc, targetsSvc, scriptExecutionSvc) {
            // Lookup lists
            $scope.projects = projectsSvc.getAll();
            $scope.scriptTypes = ["Batch", "PowerShell"];

            // Execution context
            $scope.executionContext = {};
            $scope.executionContext.targets = targetsSvc.getAll();

            $scope.isExecutionInProgress = false;
            $scope.output = "";
            $scope.appendToOutput = false;

            $scope.isExistingScript = function () {
                if ($scope.script.scriptID) {
                    return true;
                } else {
                    return false;
                }
            };

            $scope.save = function (script) {
                scriptsSvc.save(script, script.scriptID)
                    .$promise.then(
                        function (response) {
                            notificationSvc.success(script.name + " script is saved.");
                            if (script.scriptID) {
                                $location.url("Scripts");
                            } else {
                                $location.url("Script/Edit/" + response.scriptID);
                            }
                        },
                        function (response) {
                            if (response.data && (response.data instanceof Array)) {
                                notificationSvc.errors(response.data);
                                return;
                            }
                            notificationSvc.error("Script save failed.");
                        }
                    );
            };

            $scope.clearOutput = function () {
                $scope.output = "";
            };

            $scope.execute = function (script) {
                $scope.isExecutionInProgress = true;

                if (!$scope.appendToOutput) {
                    $scope.clearOutput();
                }

                var descriptor = {
                    scriptBody: script.body,
                    scriptType: script.scriptType
                };

                if ($scope.executionContext.targetID) {
                    descriptor.targetID = $scope.executionContext.targetID;
                    descriptor.remoteExecution = true;
                }

                scriptExecutionSvc.execute(descriptor).$promise.then(
                    function (response) {
                        $scope.output += response.output + "\n\n";
                        $scope.isExecutionInProgress = false;
                    },
                    function (response) {
                        if (response.data && (response.data instanceof Array)) {
                            notificationSvc.errors(response.data);
                        } else {
                            var message = "Script execution failed.";
                            notificationSvc.error(message);
                            $scope.output += message + "\n" + response + "\n\n";
                        }
                        $scope.isExecutionInProgress = false;
                    });
            };
        }
    };
});

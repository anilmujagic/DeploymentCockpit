"use strict";

app.directive("myScriptEdit", function () {
    return {
        templateUrl: "app/scripts/myScriptEdit.html",
        scope: {
            scriptID: "=scriptId",
            script: "="
        },
        controller: function ($scope, $location, scriptsSvc, notificationSvc, projectsSvc, targetsSvc, scriptExecutionSvc) {
            $scope.successKeywordsDescription =
                "These entries are used to detect successfull execution of the script in deployment job step. " +
                "Entries are evaluated line by line. If any/all (depending on 'All required' option) " +
                "entries are found in script execution output, execution is marked as successfull.";
            $scope.failureKeywordsDescription =
                "These entries are used to detect script execution failure in deployment job step. " +
                "Entries are evaluated line by line. If any/all (depending on 'All required' option) " +
                "entries are found in script execution output, execution is marked as failed, " +
                "even if Success Keywords are defined and indicate success.";

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
                            notificationSvc.saved(script.name);
                            if (script.scriptID) {
                                $location.url("Scripts");
                            } else {
                                $location.url("Script/Edit/" + response.scriptID);
                            }
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
                    scriptType: script.scriptType,
                    successKeywords: script.successKeywords,
                    failureKeywords: script.failureKeywords,
                    successKeywordsAllRequired: script.successKeywordsAllRequired,
                    failureKeywordsAllRequired: script.failureKeywordsAllRequired
                };

                if ($scope.executionContext.targetID) {
                    descriptor.targetID = $scope.executionContext.targetID;
                    descriptor.remoteExecution = true;
                }

                scriptExecutionSvc.execute(descriptor).$promise.then(
                    function (response) {
                        $scope.output += response.output;
                    },
                    function (response) {
                        $scope.output += "Script execution failed.\n" + JSON.stringify(response.data, null, "  ");
                    })
                    .finally(function () {
                        $scope.output += "\n\n";
                        $scope.isExecutionInProgress = false;
                    });
            };
        }
    };
});

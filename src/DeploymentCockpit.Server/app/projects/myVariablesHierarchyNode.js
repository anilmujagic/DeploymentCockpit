app.directive("myVariablesHierarchyNode", function (RecursionHelper) {
    return {
        templateUrl: "app/projects/myVariablesHierarchyNode.html",
        scope: {
            node: "="
        },
        compile: function (element) {
            return RecursionHelper.compile(element, function (scope, iElement, iAttrs, controller, transcludeFn) {
            });
        }
    };
});

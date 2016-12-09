// app-plans.js
(function () {

    "use strict";

    // Creating the Module
    angular.module("app-plans", ["simpleControls", "ngRoute"]).config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "plansController",
            controllerAs: "vm",
            templateUrl: "/views/plansView.html"
        });

        $routeProvider.when("/editor/:planName", {
            controller: "planEditorController",
            controllerAs: "vm",
            templateUrl: "/views/planEditorView.html"
        });

        $routeProvider.otherwise({ redirectTo: "/"})
    });

})();
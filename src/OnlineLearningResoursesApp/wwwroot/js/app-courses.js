// app-courses.js
(function () {

    "use strict";

    // Creating the Module
    angular.module("app-courses", ["simpleControls", "ngRoute"]).config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "coursesController",
            controllerAs: "vm",
            templateUrl: "/views/coursesView.html"
        });

        $routeProvider.when("/addNewCourse", {
            controller: "coursesController",
            controllerAs: "vm",
            templateUrl: "/views/addNewCourseView.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" })
    });

})();
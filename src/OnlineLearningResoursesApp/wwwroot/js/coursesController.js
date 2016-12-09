// coursesController.js

(function () {

    "use strict";

    // Getting the existing Module
    angular.module("app-courses")
    .controller("coursesController", coursesController);

    function coursesController($routeParams, $http, $route) {

        var vm = this;
        vm.courses = [];

        vm.newCourse = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/courses")
        .then(function (response) {
            // Success
            angular.copy(response.data, vm.courses);
            vm.isBusy = false;
        }, function (error) {
            // Failure
            vm.errorMessage = "Failed to load data: " + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });

        vm.addNewCourse = function () {

            vm.errorMessage = "";
            vm.isBusy = true;

            $http.post("/api/courses", vm.newCourse)
            .then(function (response) {
                // Success
                vm.courses.push(response.data);
                vm.newCourse = {};
                vm.successTextAlert = "New course has been added.";
                vm.showSuccessAlert = true;
            }, function () {
                // Failure
                vm.errorMessage = "Failed to save new course";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        vm.deleteCourse = function (id) {

            vm.errorMessage = "";
            vm.isBusy = true;

            $http.delete("/api/courses/" + id)
                    .then(function (response) {
                        // Success
                        vm.courses.splice(id, 1);
                        vm.isBusy = false;
                        $route.reload();
                    }, function () {
                        vm.errorMessage = "Failed to delete course";
                    })
            .finally(function () {
                vm.isBusy = false;
            });

        };

    };
})();
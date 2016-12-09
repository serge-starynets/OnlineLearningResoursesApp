// plansController.js

(function () {

    "use strict";

    // Getting the existing Module
    angular.module("app-plans")
    .controller("plansController", plansController);

    function plansController($http, $route, $window) {

        var vm = this;

        vm.plans = [];

        vm.newPlan = {};

        vm.errorMessage = "";
        vm.isBusy = true;


        vm.infoMessage = "";
        vm.showInfoMessage = false;

        $http.get("/api/plans")
        .then(function (response) {
            // Success
            angular.copy(response.data, vm.plans);
            if (vm.plans.length == 0) {
                vm.infoMessage = "You have not created any plan yet.";
                vm.showInfoMessage = true;
            } else {
                $route.reload;
            }
            vm.isBusy = false;
        }, function (error) {
            // Failure
            vm.errorMessage = "Failed to load data: " + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });


        vm.addPlan = function () {

            vm.errorMessage = "";
            vm.isBusy = true;

            $http.post("/api/plans", vm.newPlan)
            .then(function (response) {
                // Success
                vm.plans.push(response.data);
                vm.newPlan = {};
            }, function () {
                // Failure
                vm.errorMessage = "Failed to save new learning plan";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        vm.deletePlan = function (id, name, courses) {

            vm.errorMessage = "";
            vm.isBusy = true;
            vm.showAlertText = false;
            $http.delete("/api/plans/" + id)
                    .then(function (response) {
                        // Success
                        if (courses.length == 0) {
                            vm.plans.splice(id, 1);
                            vm.isBusy = false;
                            $route.reload();
                        } else {
                            vm.errorMessage = "Failed to delete plan";
                            vm.alertTextMessage = "You should remove all courses from the " + name + " first";
                            vm.showAlertText = true;
                        };
                    }, function () {
                            vm.errorMessage = "Failed to delete course";
                        })
            .finally(function () {
                vm.isBusy = false;
            });
        };
    }
})();
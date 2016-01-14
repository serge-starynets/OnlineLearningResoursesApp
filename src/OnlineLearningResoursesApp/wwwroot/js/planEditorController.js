// planEditorController.js
(function () {
    "use strict";

    angular.module("app-plans").controller("planEditorController", planEditorController);

    function planEditorController($routeParams, $http, $route) {
        var vm = this;

        vm.planName = $routeParams.planName;
        vm.duration;
        vm.courseName = $routeParams.name;
        vm.courses = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newCourse = {};
        vm.showInfoText = false;
        var url = "/api/plans/" + vm.planName + "/courses";

        $http.get(url)
        .then(function (response) {
            // Success
            angular.copy(response.data, vm.courses, vm.isActive);
            var durationArr = new Array;
            var duration = 0;
            for (var i in response.data) {
                durationArr.push(response.data[i]);
            }
            for (var d in durationArr) {
                duration += durationArr[d].duration;
            }
            vm.infoTextMessage = "Total duration of plan:  " + duration + " days";
            vm.showInfoText = true;
        }, function (err) {
            // Failure
            vm.errorMessage = "Failed to load courses: " + err;
        })
    .finally(function () {
        vm.isBusy = false;
    });

        vm.addCourse = function () {

            vm.isBusy = true;

            $http.post(url, vm.newCourse, vm.planName)
              .then(function (response) {
                  // success
                  vm.courses.push(response.data);
                  vm.newCourse = {};
                  $route.reload();
              }, function (err) {
                  // failure
                  vm.errorMessage = "Failed to add new course";
              })
              .finally(function () {
                  vm.isBusy = false;
              });

        };

        vm.removeCourse = function (id) {

            vm.errorMessage = "";
            vm.isBusy = true;

            $http.put(url + "/" + id)
            .then(function (response) {
                vm.courses.splice(id, 1);
                vm.isBusy = false;
                $route.reload();
            }, function (err) {
                // failure
                vm.errorMessage = "Failed to remove course from plan";
            })
              .finally(function () {
                  vm.isBusy = false;
              });
        };

        vm.startCourse = function (id) {

            $http.put(url + "/" + id)
            .then(function (response) {
                $route.reload();
            }, function (err) {
                    // failure
                    vm.errorMessage = "Failed to start course";
                })
              .finally(function () {
                  vm.isBusy = false;
            });
        };


    };

})();
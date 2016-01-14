// simpleControls.js
(function () {
    "use strict";

    angular.module("simpleControls", []).directive("waitCursor", waitCursor).directive("activeFlag", activeFlag).directive("passiveFlag", passiveFlag);

    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }

    function activeFlag() {
        return {
            scope: {
                show: "=displayWhen"
            },
            templateUrl: "/views/isActive.html"
        };
    }

    function passiveFlag() {
        return {
            scope: {
                show: "=displayWhen"
            },
            templateUrl: "/views/isPassive.html"
        };
    }

})();
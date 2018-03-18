(function () {
    'use strict';

    angular.module('app')
        .filter('startFrom',
        function () {
            return function (input, start) {
                start = +start; 
                return input.slice(start);
            }
        });

})();
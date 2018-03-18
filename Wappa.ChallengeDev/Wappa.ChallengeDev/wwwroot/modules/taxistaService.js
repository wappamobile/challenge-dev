(function () {
    'use strict';
    angular.module('app').service('taxistaService', ['$http', function ($http) {
        this.getAll = function () {
            return $http({
                method: 'GET',
                url: '/api/Taxista'
            })
        };
        this.delete = function (id) {
            return $http({
                method: 'DELETE',
                url: '/api/Taxista/' + id
            })
        }
    }]);
})();
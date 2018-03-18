(function () {
    'use strict';

    function onError(response) { };
    angular.module('app').controller('taxistaController', ['$scope', 'taxistaService', function ($scope, taxistaService) {
        var vm = this;
        vm.initCompleted = false;
        vm.model = {
            taxistas: []
        };
        vm.filterContext = {
            pageSizes: [5, 10, 25, 50, 100],
            pageSize: 5,
            currentPage: 0,
            searchText: '',
            filteredData: [],
            numberOfPages: 0,
            sortKey: 'primeiroNome',
            reverse: false,
        };
        $scope.$watch(function () {
            return vm.filterContext.searchText;
        },
            function (newValue, oldValue) {
                if (newValue === oldValue)
                    return;
                updateFilteredData();
            });
        function updateFilteredData() {
            if (vm.filterContext.searchText == '' || vm.model.taxistas.length == 0) {
                vm.filterContext.filteredData = vm.model.taxistas;
                vm.updatePages();
                return;
            }
            var filteredData = [];
            angular.forEach(vm.model.taxistas,
                function (item) {
                    for (var key in item) {
                        var modelValue = item[key];
                        if (!modelValue)
                            continue;
                        if (modelValue.toString()
                            .toLowerCase()
                            .indexOf(vm.filterContext.searchText.toString().toLowerCase()) >
                            -1) {
                            this.push(item);
                            break;
                        }
                    }
                },
                filteredData);
            vm.filterContext.filteredData = filteredData;
            vm.updatePages();
        };

        vm.updatePages = function () {
            vm.filterContext.numberOfPages = Math.ceil(vm.filterContext.filteredData.length / vm.filterContext.pageSize);
            vm.filterContext.currentPage = 0;
        };
        vm.lastPage = function () {
            vm.filterContext.currentPage = Math.ceil(vm.filterContext.filteredData.length / vm.filterContext.pageSize) - 1;
        };
        vm.nextPage = function () {
            var numberOfPages = Math.ceil(vm.filterContext.filteredData.length / vm.filterContext.pageSize) - 1;
            if (vm.filterContext.currentPage < numberOfPages)
                vm.filterContext.currentPage++;
        };
        vm.previousPage = function () {
            var numberOfPages = Math.ceil(vm.filterContext.filteredData.length / vm.filterContext.pageSize) - 1;
            if (vm.filterContext.currentPage > 0)
                vm.filterContext.currentPage--;
        };
        vm.getRange = function () {
            var arr = [];
            var total = Math.ceil(vm.filterContext.filteredData.length /
                vm.filterContext.pageSize);
            var end = total - 1;
            for (var i = 0; i <= end; i++) {
                arr.push(i);
            }
            var start = vm.filterContext.currentPage;
            if (total >= 3 && vm.filterContext.currentPage > total - 3) {
                start = total - 3;
            }
            return arr.slice(start, start + 3);
        };
        vm.sort = function (cfg) {
            vm.filterContext.sortKey = cfg;
            vm.filterContext.reverse = !vm.filterContext.reverse;
        }
        /*Data Table functions*/

        /*End Data Table functions*/
        vm.init = function () {
            taxistaService.getAll().then(function (response) {
                angular.copy(response.data, vm.model.taxistas);
                updateFilteredData();
                vm.initCompleted = true;
            }, onError);
        };
        vm.delete = function (taxista) {
            taxistaService.delete(taxista.idTaxista).then(function (response) {
                vm.model.taxistas.splice($.inArray(taxista, vm.model.taxistas), 1);
                vm.initCompleted = true;
            }, onError);
        };
    }]);

})();

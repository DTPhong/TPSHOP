(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getOrders = getOrders;
        $scope.keyword = '';
        $scope.setShipping = setShipping;
        $scope.setCancel = setCancel;
        $scope.search = search;


        function search() {
            getOrders();
        }

        function getOrders(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('No record found.');
                }
                else {
                    notificationService.displaySuccess('Found ' + result.data.TotalCount + ' records.');
                }
                $scope.orders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load order failed.');
            });
        }

        function setShipping(id) {
            var config = {
                params: {
                    id: id
                }
            }
            apiService.get('api/order/setShipping', config, function () {
                notificationService.displaySuccess('Successfully.');
                search();
            }, function () {
                notificationService.displayError('Not successfully.');
            })
        }

        function setCancel(id) {
            var config = {
                params: {
                    id: id
                }
            }
            apiService.get('api/order/setCancel', config, function () {
                notificationService.displaySuccess('Successfully.');
                search();
            }, function () {
                notificationService.displayError('Not successfully.');
            })
        }

        $scope.getOrders();
    }
})(angular.module('tpshop.orders'));
(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProduct = deleteProduct;

        $scope.selectAll = selectAll;

        $scope.isAll = false;

        $scope.deleteMulti = deleteMulti;

        function deleteMulti() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess('Deleted successfully ' + result.data + ' records.');
                search();
            }, function (error) {
                notificationService.displayError('Deleted not successfully.');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true)

        function deleteProduct(id) {
            $ngBootbox.confirm('Are you sure you want to delete this record ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/product/delete', config, function () {
                    notificationService.displaySuccess('Deleted successfully.');
                    search();
                }, function () {
                    notificationService.displayError('Deleted not successfully.');
                })
            });
        }

        function search() {
            getProducts();
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('No record found.');
                }
                else {
                    notificationService.displaySuccess('Found ' + result.data.TotalCount + ' records.');
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load product failed.');
            });
        }
        //function loadCategory() {
        //    apiService.get('api/category/getallnopaging', null, function (result) {
        //        $scope.categories = result.data;
        //    }, function () {
        //        console.log('Cannot get list category.');
        //    });
        //}
        //function loadSupplier() {
        //    apiService.get('api/supplier/getallnopaging', null, function (result) {
        //        $scope.suppliers = result.data;
        //    }, function () {
        //        console.log('Cannot get list supplier.');
        //    });
        //}
        //loadCategory();
        //loadSupplier();

        $scope.getProducts();
    }
})(angular.module('tpshop.products'));
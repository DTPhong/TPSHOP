(function (app) {
    app.controller('categoryListController', categoryListController);

    categoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function categoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.categories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCategories = getCategories;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteCategory = deleteCategory;

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
                    checkedCategories: JSON.stringify(listId)
                }
            }
            apiService.del('api/category/deletemulti', config, function (result) {
                notificationService.displaySuccess('Deleted successfully ' + result.data + ' records.');
                search();
            }, function (error) {
                notificationService.displayError('Deleted not successfully.');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.categories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.categories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("categories", function (n,o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled','disabled');
            }
        }, true)

        function deleteCategory(id) {
            $ngBootbox.confirm('Are you sure you want to delete this record ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/category/delete', config, function () {
                    notificationService.displaySuccess('Deleted successfully.');
                    search();
                }, function () {
                    notificationService.displayError('Deleted not successfully.');
                })
            });
        }

        function search() {
            getCategories();
        }

        function getCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/category/getall', config, function (result) {
                if (result.data.TotalCount==0) {
                    notificationService.displayWarning('No record found.');
                }
                else {
                    notificationService.displaySuccess('Found ' + result.data.TotalCount + ' records.');
                }
                $scope.categories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load category failed.');
            });
        }
        function loadCategoryGroup() {
            apiService.get('api/categorygroup/getall', null, function (result) {
                $scope.categoryGroups = result.data;
            }, function () {
                console.log('Cannot get list category group.');
            });
        }
        loadCategoryGroup();

        $scope.getCategories();
    }
})(angular.module('tpshop.categories'));
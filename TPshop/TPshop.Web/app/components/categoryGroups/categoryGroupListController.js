(function (app) {
    app.controller('categoryGroupListController', categoryGroupListController);

    categoryGroupListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function categoryGroupListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.categoryGroups = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCategoryGroups = getCategoryGroups;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteCategoryGroup = deleteCategoryGroup;

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
                    checkedCategoryGroups: JSON.stringify(listId)
                }
            }
            apiService.del('api/categoryGroup/deletemulti', config, function (result) {
                notificationService.displaySuccess('Deleted successfully ' + result.data + ' records.');
                search();
            }, function (error) {
                notificationService.displayError('Deleted not successfully.');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.categoryGroups, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.categoryGroups, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("categoryGroups", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true)

        function deleteCategoryGroup(id) {
            $ngBootbox.confirm('Are you sure you want to delete this record ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/categoryGroup/delete', config, function () {
                    notificationService.displaySuccess('Deleted successfully.');
                    search();
                }, function () {
                    notificationService.displayError('Deleted not successfully.');
                })
            });
        }

        function search() {
            getCategoryGroups();
        }

        function getCategoryGroups(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/categoryGroup/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('No record found.');
                }
                else {
                    notificationService.displaySuccess('Found ' + result.data.TotalCount + ' records.');
                }
                $scope.categoryGroups = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load category group failed.');
            });
        }

        $scope.getCategoryGroups();
    }
})(angular.module('tpshop.categoryGroups'));
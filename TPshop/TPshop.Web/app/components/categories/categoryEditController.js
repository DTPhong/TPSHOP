(function (app) {
    app.controller('categoryEditController', categoryEditController);

    categoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state','$stateParams','commonService'];

    function categoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.category = {
            CreatedDate: new Date(),
            Status: true,
            Homeflag: false
        }
        $scope.UpdateCategory = UpdateCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }

        function loadCategoryDetail() {
            apiService.get('/api/category/getbyid/' + $stateParams.id,null, function (result) {
                $scope.category = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCategory() {
            apiService.put('/api/category/update', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is updated.');
                $state.go('categories');
            }, function (error) {
                notificationService.displayError('Updated is not successful.');
            });
        }
        function loadCategoryGroup() {
            apiService.get('/api/categorygroup/getall', null, function (result) {
                $scope.categoryGroups = result.data;
            }, function () {
                console.log('Cannot get list category group.');
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.category.Image = fileUrl;
                })
            }
            finder.popup();
        }
        loadCategoryGroup();
        loadCategoryDetail();
    }
})(angular.module('tpshop.categories'));
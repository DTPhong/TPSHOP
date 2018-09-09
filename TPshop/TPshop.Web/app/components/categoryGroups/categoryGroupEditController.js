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
            apiService.get('/api/categorygroup/getbyid/' + $stateParams.id,null, function (result) {
                $scope.category = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCategory() {
            apiService.put('/api/categorygroup/update', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is updated.');
                $state.go('categories');
            }, function (error) {
                notificationService.displayError('Updated is not successful.');
            });
        }
        loadCategoryDetail();
    }
})(angular.module('tpshop.categoryGroups'));
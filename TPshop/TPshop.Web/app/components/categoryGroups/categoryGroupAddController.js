(function (app) {
    app.controller('categoryAddController', categoryAddController);

    categoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function categoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.category = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.AddCategoryGroup = AddCategoryGroup;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }
        function AddCategoryGroup() {
            apiService.post('/api/categorygroup/create', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is added.');
                $state.go('categoryGroups');
            }, function (error) {
                notificationService.displayError('Added is not successful.');
            });
        }
    }
})(angular.module('tpshop.categoryGroups'));
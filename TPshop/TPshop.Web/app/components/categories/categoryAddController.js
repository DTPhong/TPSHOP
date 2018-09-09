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
        $scope.AddCategory = AddCategory;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }
        function AddCategory() {
            apiService.post('/api/category/create', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is added.');
                $state.go('categories');
            }, function (error) {
                notificationService.displayError('Added is not successful.');
            });
        }
        function loadCategoryGroup() {
            apiService.get('/api/categorygroup/getallnopaging', null, function (result) {
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
    }
})(angular.module('tpshop.categories'));
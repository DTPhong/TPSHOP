(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
            Homeflag: false
        }
        $scope.AddProduct = AddProduct;

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function AddProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is added.');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Added is not successful.');
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        $scope.moreImages = [];

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }
        function loadCategoryGroup() {
            apiService.get('/api/category/getallnopaging', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('Cannot get list category group.');
            });
        }
        function loadSupplier() {
            apiService.get('/api/supplier/getallnopaging', null, function (result) {
                $scope.suppliers = result.data;
            }, function () {
                console.log('Cannot get list supplier group.');
            });
        }
        loadSupplier();
        loadCategoryGroup();
    }
})(angular.module('tpshop.products'));
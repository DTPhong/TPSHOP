(function (app) {
    app.controller('productDetailAddController', productDetailAddController);

    productDetailAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function productDetailAddController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product = {};
        $scope.UpdateProduct = UpdateProduct;

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                console.log(result.data);
                $scope.product = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        loadProductDetail();
    }
})(angular.module('tpshop.products'));
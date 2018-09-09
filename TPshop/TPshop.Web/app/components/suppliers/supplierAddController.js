(function (app) {
    app.controller('supplierAddController', supplierAddController);

    supplierAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function supplierAddController(apiService, $scope, notificationService, $state) {
        $scope.supplier = {}
        $scope.AddSupplier = AddSupplier;
        
        function AddSupplier() {
            apiService.post('/api/supplier/create', $scope.supplier, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is added.');
                $state.go('suppliers');
            }, function (error) {
                notificationService.displayError('Added is not successful.');
            });
        }
    }
})(angular.module('tpshop.suppliers'));
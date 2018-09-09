(function (app) {
    app.controller('supplierEditController', supplierEditController);

    supplierEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function supplierEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.supplier = {}
        $scope.UpdateSupplier = UpdateSupplier;

        function loadSupplierDetail() {
            apiService.get('/api/supplier/getbyid/' + $stateParams.id, null, function (result) {
                $scope.supplier = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSupplier() {
            apiService.put('/api/supplier/update', $scope.supplier, function (result) {
                notificationService.displaySuccess(result.data.Name + ' is updated.');
                $state.go('suppliers');
            }, function (error) {
                notificationService.displayError('Updated is not successful.');
            });
        }
        loadSupplierDetail();
    }
})(angular.module('tpshop.suppliers'));
(function () {
    angular.module('tpshop.suppliers', ['tpshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('suppliers', {
            url: "/suppliers",
            templateUrl: "/app/components/suppliers/supplierListView.html",
            parent: 'base',
            controller: "supplierListController"
        }).state('supplier_add', {
            url: "/supplier_add",
            templateUrl: "/app/components/suppliers/supplierAddView.html",
            parent: 'base',
            controller: "supplierAddController"
        }).state('supplier_edit', {
            url: "/supplier_edit/:id",
            templateUrl: "/app/components/suppliers/supplierEditView.html",
            parent: 'base',
            controller: "supplierEditController"
        });
    }
})();
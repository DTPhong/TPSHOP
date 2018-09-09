(function () {
    angular.module('tpshop.orders', ['tpshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('orders', {
            url: "/orders",
            templateUrl: "/app/components/orders/orderListView.html",
            parent: 'base',
            controller: "orderListController"
        });
    }
})();
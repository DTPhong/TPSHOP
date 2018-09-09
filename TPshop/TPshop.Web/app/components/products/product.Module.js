(function () {
    angular.module('tpshop.products', ['tpshop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
                url: "/products",
                parent: 'base',
                templateUrl: "/app/components/products/productListView.html",
                controller: "productListController"
            }).state('product_add', {
                url: "/product_add",
                parent: 'base',
                templateUrl: "/app/components/products/productAddView.html",
                controller: "productAddController"
            }).state('product_edit', {
                url: "/product_edit/:id",
                templateUrl: "/app/components/products/productEditView.html",
                parent: 'base',
                controller: "productEditController"
            }).state('productDetail_add', {
                url: "/productDetail_add/:id",
                templateUrl: "/app/components/products/productDetailAddView.html",
                parent: 'base',
                controller: "productDetailAddController"
            });
    }
})();
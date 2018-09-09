(function () {
    angular.module('tpshop.categories', ['tpshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('categories', {
            url: "/categories",
            templateUrl: "/app/components/categories/categoryListView.html",
            parent: 'base',
            controller: "categoryListController"
        }).state('category_add', {
            url: "/category_add",
            templateUrl: "/app/components/categories/categoryAddView.html",
            parent: 'base',
            controller: "categoryAddController"
        }).state('category_edit', {
            url: "/category_edit/:id",
            templateUrl: "/app/components/categories/categoryEditView.html",
            parent: 'base',
            controller: "categoryEditController"
        });
    }
})();
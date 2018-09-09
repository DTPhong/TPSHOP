(function () {
    angular.module('tpshop.categoryGroups', ['tpshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('categoryGroups', {
            url: "/categoryGroups",
            templateUrl: "/app/components/categoryGroups/categoryGroupListView.html",
            parent: 'base',
            controller: "categoryGroupListController"
        }).state('categoryGroup_add', {
            url: "/categoryGroup_add",
            templateUrl: "/app/components/categoryGroups/categoryGroupAddView.html",
            parent: 'base',
            controller: "categoryGroupAddController"
        }).state('categoryGroup_edit', {
            url: "/categoryGroup_edit/:id",
            templateUrl: "/app/components/categoryGroups/categoryGroupEditView.html",
            parent: 'base',
            controller: "categoryGroupEditController"
        });
    }
})();
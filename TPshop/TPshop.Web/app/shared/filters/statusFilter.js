(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true) {
                return 'Actived';
            }
            else {
                return 'Locked';
            }
        }
    });
    app.filter('homeflagFilter', function () {
        return function (input) {
            if (input == true) {
                return 'Show';
            }
            else {
                return 'Hide';
            }
        }
    });
    app.filter('orderFilter', function () {
        return function (input) {
            if (input == true) {
                return 'Shipping';
            }
            else if (input == null) {
                return 'Processing';
            }
            else {
                return 'Cancel';
            }
        }
    });
})(angular.module('tpshop.common'));
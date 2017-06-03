angular
    .module('manager')
    .controller('TableController', ['$scope', '$http', function ($scope, $http) {
        $http.post('/api/demo/table',
            { "Method": 0, "Parameters": [{ "Name": "pageSize", "Value": "50" }] }
        ).then(function (response) {
            $scope.data = response.data;
        }, function () {
        });
    }]);
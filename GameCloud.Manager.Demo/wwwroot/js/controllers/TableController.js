angular
    .module('manager')
    .controller('TableController', ['$scope', '$http', function ($scope, $http) {
        var d =
            $http.post('/api/demo/table',
                { "Method": 0, "Parameters": [{ "Name": "pageSize", "Value": "50" }] }
            ).then(function (response) {
                $scope.data = response.data;
                console.log('aaa');
                debugger;   

            }, function () {
            });
    }]);
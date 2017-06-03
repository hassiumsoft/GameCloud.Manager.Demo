angular
    .module('manager')
    .controller('FormController', ['$scope', '$http', function ($scope, $http) {
        $http.post('/api/demo/form', { "formData1": "132", "formData2": "456" }).then(function (response) {
            $scope.data = response.data;
        }, function () {
        });
    }]);
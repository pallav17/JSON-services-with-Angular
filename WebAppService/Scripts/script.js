var app = angular
    .module("myModule", [])
    .controller("myController", function ($scope, $http) {

        $http.get('WebService1.asmx/getEmployee')
            .then(function (response) {

                $scope.employee = response.data;

            });





    });
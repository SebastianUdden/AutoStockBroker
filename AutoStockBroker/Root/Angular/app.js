var app = angular.module('AutoStockBroker', []);

app.run(function ($rootScope) {
    $rootScope.myName = "Sebastian Uddén";
});

app.controller('HomeController', function ($scope, $http) {
    $scope.name = 'AngularWorld';

    $http.get('/Root/Json/testJson.txt').success(function (data) {
        $scope.tarzan = data;
    });
});
app.controller('PrototypeController', function ($scope, $http) {
    $scope.name = 'PrototypeWorld';    

    $http.get('/Root/Json/tarzan.json')
         .success(function (data) {
             $scope.tarzan = data.name;
             //$scope.Stocks = data.Stocks;
         })
         .error(function (data, status, headers, config) {
             //  Do some error handling here
         });

    $scope.Stocks = null;

    $http.get('/Root/Json/stocks.json')
         .success(function (data) {
             $scope.Stocks = data.Stocks;
         })
         .error(function (data, status, headers, config) {
             //  Do some error handling here
         });

    $scope.sortType = 'StringValue'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order
    $scope.search = '';     // set the default search/filter term
});
    

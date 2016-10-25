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

    $scope.saveToPc = function (data, filename) {

        alert("Download not working yet!")

        if (!data) {
            console.error('No data');
            return;
        }

        if (!filename) {
            filename = 'download.json';
        }

        if (typeof data === 'object') {
            data = JSON.stringify(data, undefined, 2);
        }

        var blob = new Blob([data], { type: 'text/json' });


        //var data = { a: 1, b: 2, c: 3 };
        //var json = JSON.stringify(data);
        //var blob = new Blob([json], { type: "application/json" });
        var url = URL.createObjectURL(blob);

        var a = document.createElement('a');
        a.download = "backup.json";
        a.href = url;
        a.textContent = "Download backup.json";
    };

    // Item List Arrays
    //$scope.items = [];
    $scope.myStocks = [];

    // Add a Item to the list
    //$scope.addItem = function () {

    //    $scope.items.push({
    //        amount: $scope.itemAmount,
    //        name: $scope.itemName
    //    });

    //    // Clear input fields after push
    //    $scope.itemAmount = "";
    //    $scope.itemName = "";

    //};

    $scope.addStock = function (index) {
        if ($scope.myStocks.length === 0) {
            $scope.myStocks.push($scope.Stocks[index]);
            $scope.myStocks[0].AmountOwned++;
        }
        else {
            stockExists = false;
            indexNumber = 0;
            for (var i = 0; i < $scope.myStocks.length; i++) {
                if ($scope.myStocks[i].Name === $scope.Stocks[index].Name) {
                    stockExists = true;
                }
                indexNumber = i;
            }
            if (stockExists) {
                $scope.myStocks[indexNumber].AmountOwned++;
            }
            else {
                $scope.myStocks.push($scope.Stocks[index]);

                for (var i = 0; i < $scope.myStocks.length; i++) {
                    if ($scope.Stocks[index].Name === $scope.myStocks[i].Name) {
                        $scope.myStocks[i].AmountOwned++;
                    }
                }
            }
        }

        $scope.myStocks.TotalValue = 0;
        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.myStocks[i].ValueOwned = $scope.myStocks[i].ValueDouble * $scope.myStocks[i].AmountOwned;
            $scope.myStocks.TotalValue += $scope.myStocks[i].ValueOwned;
        }
        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.myStocks[i].Weight =  $scope.myStocks[i].ValueOwned / $scope.myStocks.TotalValue;
        }
    };

    $scope.removeStock = function (index) {
        if ($scope.myStocks[index].AmountOwned > 1) {
            $scope.myStocks[index].AmountOwned--;
        }
        else {
            $scope.myStocks.splice(index, 1);
        }
    };

    // Get Total Stocks Count
    $scope.getTotalStocksCount = function () {
        return $scope.Stocks.length;
    };

    // Get myStocks Count
    $scope.getMyStocksCount = function () {
        return $scope.myStocks.length;
    };



    $scope.$watch('search.ValueDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.ValueDouble = "";
    });

    $scope.$watch('search.AmountOwned', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.AmountOwned = "";
    });

    $scope.$watch('search.ValueOwned', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.ValueOwned = "";
    });

    $scope.$watch('search.Weight', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.Weight = "";
    });

    $scope.$watch('search.MarketCapInt', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.MarketCapInt = "";
    });

    $scope.$watch('search.DividendDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.DividendDouble = "";
    });

    $scope.$watch('search.Volatility', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.Volatility = "";
    });

    $scope.$watch('search.BetaDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.BetaDouble = "";
    });

    $scope.$watch('search.PriceEarningsDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.PriceEarningsDouble = "";
    });

    $scope.$watch('search.PriceSalesDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.PriceSalesDouble = "";
    });

    $scope.$watch('search.ConsensusDouble', function (newValue, oldValue) {
        if (newValue === null)
            $scope.search.ConsensusDouble = "";
    });
});


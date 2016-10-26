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
    $scope.fileSaved = false;
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

    //$scope.uploadedFile = function (element) {
    //    $scope.$apply(function ($scope) {
    //        $scope.myStocks = element.files;
    //    });
    //};

    $scope.addFile = function () {
        var input, file, fr;

        if (typeof window.FileReader !== 'function') {
            alert("The file API isn't supported on this browser yet.");
            return;
        }

        input = document.getElementById('fileinput');
        if (!input) {
            alert("Um, couldn't find the fileinput element.");
        }
        else if (!input.files) {
            alert("This browser doesn't seem to support the `files` property of file inputs.");
        }
        else if (!input.files[0]) {
            alert("Please select a file before clicking 'Load'");
        }
        else {
            file = input.files[0];
            fr = new FileReader();
            fr.onload = receivedText;
            fr.readAsText(file);
        }

        function receivedText(e) {
            lines = e.target.result;
            $scope.myStocks = JSON.parse(lines);
        }
    }

    $scope.saveToPc = function (data, filename) {

        var theData = $scope.myStocks;
        var theJSON = JSON.stringify(theData);
        var uri = "data:application/json;charset=UTF-8," + encodeURIComponent(theJSON);

        var a = document.getElementById('saveToFile');
        a.href = uri;
        $scope.fileSaved = true;
    };

    $scope.myStocks = [];

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
                    $scope.myStocks[i].AmountOwned++;
                }
            }
            if (stockExists) {
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
            $scope.myStocks[i].Weight = $scope.myStocks[i].ValueOwned / $scope.myStocks.TotalValue;
        }
    };

    $scope.removeStock = function (index) {
        if ($scope.myStocks[index].AmountOwned > 1) {
            $scope.myStocks[index].AmountOwned--;
        }
        else {
            $scope.myStocks[index].AmountOwned--;
            $scope.myStocks.splice(index, 1);
        }

        $scope.myStocks.TotalValue = 0;
        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.myStocks[i].ValueOwned = $scope.myStocks[i].ValueDouble * $scope.myStocks[i].AmountOwned;
            $scope.myStocks.TotalValue += $scope.myStocks[i].ValueOwned;
        }
        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.myStocks[i].Weight = $scope.myStocks[i].ValueOwned / $scope.myStocks.TotalValue;
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

    $scope.$watch('myStocks', function (newValue, oldValue) {
        
    });

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


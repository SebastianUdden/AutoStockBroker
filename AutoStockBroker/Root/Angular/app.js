var app = angular.module('AutoStockBroker', ["chart.js"]);

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
    $scope.myStocksSaved = false;
    $scope.StocksSaved = false;
    $scope.name = 'PrototypeWorld';

    $scope.pieLabels = [];
    $scope.pieData = [];
    //$scope.pieData = [300, 500, 100];
    //$scope.pieLabels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    

    $scope.Stocks = null;

    $http.get('/Root/Json/stocks.json')
         .success(function (data) {
             $scope.Stocks = data.Stocks;
         })
         .error(function (data, status, headers, config) {
             //  Do some error handling here
         });

    $scope.GreaterThan = true;
    $scope.sortType = 'StringValue'; // set the default sort type
    $scope.sortReverse = false;  // set the default sort order
    $scope.search = '';     // set the default search/filter term

    $scope.ToggleGreaterThanValueDouble = function () {
        $scope.GreaterThanValueDouble = !$scope.GreaterThanValueDouble;
    };

    $scope.ToggleGreaterThanAmountOwned = function () {
        $scope.GreaterThanAmountOwned = !$scope.GreaterThanAmountOwned;
    };

    $scope.ToggleGreaterThanValueOwned = function () {
        $scope.GreaterThanValueOwned = !$scope.GreaterThanValueOwned;
    };

    $scope.ToggleGreaterThanWeight = function () {
        $scope.GreaterThanWeight = !$scope.GreaterThanWeight;
    };

    $scope.ToggleGreaterThanMarketCap = function () {
        $scope.GreaterThanMarketCap = !$scope.GreaterThanMarketCap;
    };
    $scope.ToggleGreaterThanDividend = function () {
        $scope.GreaterThanDividend = !$scope.GreaterThanDividend;
    };

    $scope.ToggleGreaterThanVolatility = function () {
        $scope.GreaterThanVolatility = !$scope.GreaterThanVolatility;
    };

    $scope.ToggleGreaterThanBeta = function () {
        $scope.GreaterThanBeta = !$scope.GreaterThanBeta;
    };

    $scope.ToggleGreaterThanPriceEarnings = function () {
        $scope.GreaterThanPriceEarnings = !$scope.GreaterThanPriceEarnings;
    };

    $scope.ToggleGreaterThanPriceSales = function () {
        $scope.GreaterThanPriceSales = !$scope.GreaterThanPriceSales;
    };

    $scope.ToggleGreaterThanConsensus = function () {
        $scope.GreaterThanConsensus = !$scope.GreaterThanConsensus;
    };



    $scope.ToggleGreaterThanVD = function () {
        $scope.GreaterThanVD = !$scope.GreaterThanVD;
    };

    $scope.ToggleGreaterThanAO = function () {
        $scope.GreaterThanAO = !$scope.GreaterThanAO;
    };

    $scope.ToggleGreaterThanVO = function () {
        $scope.GreaterThanVO = !$scope.GreaterThanVO;
    };

    $scope.ToggleGreaterThanW = function () {
        $scope.GreaterThanW = !$scope.GreaterThanW;
    };

    $scope.ToggleGreaterThanMC = function () {
        $scope.GreaterThanMC = !$scope.GreaterThanMC;
    };
    $scope.ToggleGreaterThanD = function () {
        $scope.GreaterThanD = !$scope.GreaterThanD;
    };

    $scope.ToggleGreaterThanV = function () {
        $scope.GreaterThanV = !$scope.GreaterThanV;
    };

    $scope.ToggleGreaterThanB = function () {
        $scope.GreaterThanB = !$scope.GreaterThanB;
    };

    $scope.ToggleGreaterThanPE = function () {
        $scope.GreaterThanPE = !$scope.GreaterThanPE;
    };

    $scope.ToggleGreaterThanPS = function () {
        $scope.GreaterThanPS = !$scope.GreaterThanPS;
    };

    $scope.ToggleGreaterThanC = function () {
        $scope.GreaterThanC = !$scope.GreaterThanC;
    };


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

        if (filename === 'myStocks.json') {
            var theData = $scope.myStocks;
            var theJSON = JSON.stringify(theData);
            var uri = "data:application/json;charset=UTF-8," + encodeURIComponent(theJSON);
            var a = document.getElementById('saveToMyStocks');
            a.href = uri;
            $scope.myStocksSaved = true;
        }
        else if (filename === 'Stocks.json') {
            var theData = $scope.Stocks;
            var theJSON = JSON.stringify(theData);
            var uri = "data:application/json;charset=UTF-8," + encodeURIComponent(theJSON);
            var a = document.getElementById('saveToStocks');
            a.href = uri;
            $scope.StocksSaved = true;
        }
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
        };
        $scope.CalculateAmountValueAndWeight();
    };
    $scope.removeStock = function (stockName) {
        for (var i = 0; i < $scope.myStocks.length; i++) {
            if ($scope.myStocks[i].Name === stockName) {
                if ($scope.myStocks[i].AmountOwned > 1) {
                    $scope.myStocks[i].AmountOwned--;
                }
                else {
                    $scope.myStocks[i].AmountOwned--;
                    $scope.myStocks.splice(i, 1);
                }
            }
        }
        $scope.CalculateAmountValueAndWeight();
    };

    $scope.addOneMoreStock = function (index) {
        $scope.myStocks[index].AmountOwned++;
        $scope.CalculateAmountValueAndWeight();
    };
    $scope.removeOneMoreStock = function (index) {
        $scope.myStocks[index].AmountOwned--;
        if ($scope.myStocks[index].AmountOwned < 1) {
            $scope.myStocks.splice(index, 1);
        }
        $scope.CalculateAmountValueAndWeight();
    };

    $scope.CalculateAmountValueAndWeight = function () {
        $scope.pieLabels = [];
        $scope.pieData = [];

        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.pieLabels.Add($scope.myStocks[i].Name);
            $scope.pieData.Add($scope.myStocks[i].ValueDouble)
        }

        $scope.myStocks.TotalValue = 0;
        $scope.myStocks.TotalAmountOwned = 0;
        $scope.myStocks.TotalVolatility = 0;
        $scope.myStocks.TotalWeight = 0;
        for (var i = 0; i < $scope.myStocks.length; i++) {
            if ($scope.AmountOwned === 0) {
                $scope.AmountOwned++;
            }
            $scope.myStocks[i].ValueOwned = $scope.myStocks[i].ValueDouble * $scope.myStocks[i].AmountOwned;
            $scope.myStocks.TotalValue += $scope.myStocks[i].ValueOwned;
            $scope.myStocks.TotalAmountOwned += $scope.myStocks[i].AmountOwned;
            $scope.myStocks.TotalVolatility += $scope.myStocks[i].Volatility;
        }
        for (var i = 0; i < $scope.myStocks.length; i++) {
            $scope.myStocks[i].Weight = $scope.myStocks[i].ValueOwned / $scope.myStocks.TotalValue;
            $scope.myStocks.TotalWeight += $scope.myStocks[i].Weight;
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

    $scope.myVD = {};
    $scope.filter_byValueDouble = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myVD['__' + field];
            return;
        }
        $scope.myVD['__' + field] = true;
        if ($scope.GreaterThanValueDouble) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myAO = {};
    $scope.filter_byAmountOwned = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myAO['__' + field];
            return;
        }
        $scope.myAO['__' + field] = true;
        if ($scope.GreaterThanAmountOwned) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myVO = {};
    $scope.filter_byValueOwned = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myVO['__' + field];
            return;
        }
        $scope.myVO['__' + field] = true;
        if ($scope.GreaterThanValueOwned) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myW = {};
    $scope.filter_byWeight = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myW['__' + field];
            return;
        }
        $scope.myW['__' + field] = true;
        if ($scope.GreaterThanWeight) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myMC = {};
    $scope.filter_byMarketCap = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myMC['__' + field];
            return;
        }
        $scope.myMC['__' + field] = true;
        if ($scope.GreaterThanMarketCap) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myD = {};
    $scope.filter_byDividend = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myD['__' + field];
            return;
        }
        $scope.myD['__' + field] = true;
        if ($scope.GreaterThanDividend) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myV = {};
    $scope.filter_byVolatility = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myV['__' + field];
            return;
        }
        $scope.myV['__' + field] = true;
        if ($scope.GreaterThanVolatility) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myB = {};
    $scope.filter_byBeta = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myB['__' + field];
            return;
        }
        $scope.myB['__' + field] = true;
        if ($scope.GreaterThanBeta) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myPE = {};
    $scope.filter_byPriceEarnings = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myPE['__' + field];
            return;
        }
        $scope.myPE['__' + field] = true;
        if ($scope.GreaterThanPriceEarnings) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myPS = {};
    $scope.filter_byPriceSales = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myPS['__' + field];
            return;
        }
        $scope.myPS['__' + field] = true;
        if ($scope.GreaterThanPriceSales) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };

    $scope.myC = {};
    $scope.filter_byConsensus = function (field) {
        if ($scope.myG[field] === null) {
            $scope.myG[field] = '';
            delete $scope.myC['__' + field];
            return;
        }
        $scope.myC['__' + field] = true;
        if ($scope.GreaterThanConsensus) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.myG[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.myG[field]; })
        }
    };




    $scope.VD = {};
    $scope.filter_byVD = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.VD['__' + field];
            return;
        }
        $scope.VD['__' + field] = true;
        if ($scope.GreaterThanVD) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.AO = {};
    $scope.filter_byAO = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.AO['__' + field];
            return;
        }
        $scope.AO['__' + field] = true;
        if ($scope.GreaterThanAO) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.VO = {};
    $scope.filter_byVO = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.VO['__' + field];
            return;
        }
        $scope.VO['__' + field] = true;
        if ($scope.GreaterThanVO) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.W = {};
    $scope.filter_byW = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.W['__' + field];
            return;
        }
        $scope.W['__' + field] = true;
        if ($scope.GreaterThanW) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.MC = {};
    $scope.filter_byMC = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.MC['__' + field];
            return;
        }
        $scope.MC['__' + field] = true;
        if ($scope.GreaterThanMC) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.D = {};
    $scope.filter_byD = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.D['__' + field];
            return;
        }
        $scope.D['__' + field] = true;
        if ($scope.GreaterThanD) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.V = {};
    $scope.filter_byV = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.V['__' + field];
            return;
        }
        $scope.V['__' + field] = true;
        if ($scope.GreaterThanV) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.B = {};
    $scope.filter_byB = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.B['__' + field];
            return;
        }
        $scope.B['__' + field] = true;
        if ($scope.GreaterThanBeta) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.PE = {};
    $scope.filter_byPE = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.PE['__' + field];
            return;
        }
        $scope.PE['__' + field] = true;
        if ($scope.GreaterThanPE) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.PS = {};
    $scope.filter_byPS = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.PS['__' + field];
            return;
        }
        $scope.PS['__' + field] = true;
        if ($scope.GreaterThanPS) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.C = {};
    $scope.filter_byC = function (field) {
        if ($scope.g[field] === null) {
            $scope.g[field] = '';
            delete $scope.C['__' + field];
            return;
        }
        $scope.C['__' + field] = true;
        if ($scope.GreaterThanC) {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] > $scope.g[field]; })
        } else {
            $scope.Stocks.forEach(function (v) { v['__' + field] = v[field] < $scope.g[field]; })
        }
    };

    $scope.$watch('myStocks', function (newValue, oldValue) {

    });

    $scope.$watch('GreaterThan', function (newValue, oldValue) {
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

    $scope.GreaterThanValueDouble = true;
    $scope.GreaterThanAmountOwned = true;
    $scope.GreaterThanValueOwned = true;
    $scope.GreaterThanWeight = true;
    $scope.GreaterThanMarketCap = true;
    $scope.GreaterThanDividend = true;
    $scope.GreaterThanVolatility = true;
    $scope.GreaterThanBeta = true;
    $scope.GreaterThanPriceEarnings = true;
    $scope.GreaterThanPriceSales = true;
    $scope.GreaterThanConsensus = true;

    $scope.GreaterThanVD = true;
    $scope.GreaterThanAO = true;
    $scope.GreaterThanVO = true;
    $scope.GreaterThanW = true;
    $scope.GreaterThanMC = true;
    $scope.GreaterThanD = true;
    $scope.GreaterThanV = true;
    $scope.GreaterThanB = true;
    $scope.GreaterThanPE = true;
    $scope.GreaterThanPS = true;
    $scope.GreaterThanC = true;
});



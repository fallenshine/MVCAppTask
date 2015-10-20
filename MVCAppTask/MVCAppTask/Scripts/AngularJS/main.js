angular.module('SPAApp', [])
        .controller('LPController', function ($scope, $http) {

            //Makes GET request to Loads Data To Page
            $scope.loadData = function (Action) {

                if (Action == 'Redirect') {
                    $scope.working = 'default';
                    $scope.message = "Loading...";
                }
                $scope.mortMessage = '';

                $http.get("/api/virtual/GetLandingProperties")
                    .success(function (data) {
                        if (Action == 'Redirect') {
                            $scope.working = 'main';
                        }
                        $scope.landProperties = data;
                    })
                    .error(function (data) {
                        $scope.message = "Wops... There is something wrong with the API!";
                        $scope.working = 'default';
                    });
            };

            //Triggers Change to Add/Edit Panel
            $scope.editData = function (data) {
                $scope.working = 'addNEdit';
                $scope.changeData = data
                $scope.AddNew = false;
            };

            //Triggers Change to Add/Edit Panel
            $scope.addData = function () {
                $scope.working = 'addNEdit';
                $scope.changeData = '';
                $scope.AddNew = true;
            };

            //Triggers Reset to Main Panel
            $scope.reset = function () {
                $scope.working = 'main';
                $scope.changeData = '';
                $scope.mortMessage = '';
            };

            //Makes POST request to add/update an Land Prop.
            $scope.saveLandProp = function (dataToSend) {
                if ($scope.AddNew == true) {
                    $http.post("/api/virtual/AddLandProperty", dataToSend)
                        .success(function (data) {
                            $scope.loadData('Redirect');
                        }).error(function (data) {
                            $scope.message = "Wops... There is something wrong with the API!";
                            $scope.working = 'default';
                        });
                }
                else {
                    $http.post("/api/virtual/UpdateLandProperty", dataToSend)
                        .success(function (data) {
                            $scope.loadData('Redirect');
                        }).error(function (data) {
                            $scope.message = "Wops... There is something wrong with the API!";
                            $scope.working = 'default';
                        });
                }
            };

            //Makes POST request to update an Mortgage
            $scope.saveMortgage = function (dataToSend) {
                $http.post("/api/virtual/UpdateMortage", dataToSend)
                    .success(function (data) {
                        $scope.loadData('NoRedirect');
                        $scope.mortMessage = 'Morgage was updated!';
                    }).error(function (data) {
                        $scope.mortMessage = 'Morgage couldnt be updated!';
                    });
            };

            //Makes POST request to add an Mortgage
            $scope.addMortgage = function (LandPropertiyID, Date, MoneyRecieved) {
                $http.post("/api/virtual/AddMortage", {
                    'LandPropertiyID': LandPropertiyID,
                    'Date': Date,
                    'MoneyRecieved': MoneyRecieved
                })
                    .success(function (data) {
                        $scope.loadData('Redirect');
                    }).error(function (data) {
                        $scope.mortMessage = 'Morgage couldnt be updated!';
                    });
            };

            //Makes POST request to delete an LandPropertiy
            $scope.deleteLandProp = function (dataToSend) {
                $http.post("/api/virtual/DeleteLandPropertiy", dataToSend)
                    .success(function (data) {
                        $scope.loadData('Redirect');
                    }).error(function (data) {
                        $scope.message = "Wops... There is something wrong with the API!";
                        $scope.working = 'default';
                    });
            };

            //Makes POST request to delete an Mortgage
            $scope.deleteMortgage = function (dataToSend) {
                $http.post("/api/virtual/DeleteMortgage", dataToSend)
                    .success(function (data) {
                        $scope.loadData('Redirect');
                    }).error(function (data) {
                        $scope.mortMessage = 'Morgage couldnt be updated!';
                    });
            };
        });


app.controller('taskController', ['$scope', 'taskService',
    function ($scope, taskService) {

        $scope.data = [];
        $scope.taskList = [];
        // Get Task List

        function loadTask()
        {
            taskService.GetTaskList().then(
                function (response) {
                    $scope.taskList = response.data;
                },
                //function (error) {
                //    httpErrorService.ThrowError(error);
                //}
            );
        }
        loadTask();

        // Read from Excel Sheet
        $scope.READ = function () {
            /*Checks whether the file is a valid excel file*/
            //var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
            var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
            if ($("#ngexcelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
                xlsxflag = true;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;
                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }
                var sheet_name_list = workbook.SheetNames;
                var cnt = 0;
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                    if (xlsxflag) {
                        var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    }
                    else {
                        var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }

                    if (exceljson.length > 0) {
                        for (var i = 0; i < exceljson.length; i++) {

                            $scope.taskData = {};
                            $scope.taskData.TaskId = i;
                            $scope.taskData.WorkOrder = exceljson[i].WO;
                            $scope.taskData.WorkType = exceljson[i].WorkType;
                            $scope.taskData.Client = exceljson[i].Client;
                            $scope.taskData.ClientDueDate = exceljson[i].ClientDateDue;

                            $scope.taskData.AssignTo = 0;
                            $scope.taskData.TaskName = '';
                            $scope.taskData.Description = '';
                            $scope.taskData.Status = 0;
                            $scope.taskData.TaskCreateDate = exceljson[i].ClientDateDue;

                            $scope.data.push($scope.taskData);
                            $scope.$apply();
                        }
                    }
                });
            }
            if (xlsxflag) {
                reader.readAsArrayBuffer($("#ngexcelfile")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#ngexcelfile")[0].files[0]);
            }
        };

        // Save Data
        $scope.SaveData = function () {

                //for (var i = 0; i < $scope.data.length; i++) {

            taskService.CreateTask($scope.data).then(
                        function (response) {
                            //console.log(response.status);
                            if (response.data.status === true) {
                                alert("successfully saved");
                                $scope.clear();
                            }
                            else{
                                alert('Data already exists.');
                            }
                        });
                //}
        };

        // Edit Data
        $scope.EditData = function (id) {
            var editData = _.filter($scope.data, function (num) { return num.TaskId === id; });
            $scope.editData = editData[0];
            $scope.showPoppup();
        };
        $scope.showPoppup = function () {
            $('#taskEditPopupModal').modal('show');
        };
        $scope.closePoppup = function () {
            $('#taskEditPopupModal').modal('hide');
        };

        // Update Data
        $scope.UpdateData = function (id) {

        }

        // Delete Data
        $scope.DeleteData = function (index) {
            $scope.data.splice(index, 1); 
        };

}]);
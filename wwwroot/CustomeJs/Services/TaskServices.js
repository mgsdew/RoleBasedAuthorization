
var baseUrl = $("base").first().attr("href");

app.factory('taskService', function ($http, $location, $window) {
    var fac = {};
    var baseUrl = new $window.URL($location.absUrl()).origin;
    fac.CreateTask = function (task) {
        return $http.post(baseUrl + "/api/Task/CreateTask/", task);
    };
    fac.GetTaskList = function () {
        return $http.get(baseUrl + "/api/Task/TaskList/");
    };
    return fac;
});
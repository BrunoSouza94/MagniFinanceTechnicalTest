app.controller("StudentController", function ($scope, $http) {
    $http.get("/student/GetStudent")
        .then((res) => {
            console.log(res);
        });
});
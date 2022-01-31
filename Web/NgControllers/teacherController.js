app.controller("TeacherController", function ($scope, $http) {
    $scope.insertedTeacher = {};
    $scope.currentId = null;
    $scope.isEdit = false;

    $scope.getTeachers = function () {
        $http.get("/Teacher/GetTeachers")
            .then((res) => {
                $scope.Teachers = res.data;
            });
    };

    $scope.insertOrEditTeacher = function () {
        if ($scope.insertedTeacher && $scope.insertedTeacher.FirstName && !$scope.isEdit) {
            $http.post("/Teacher/Insert", $scope.insertedTeacher)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getTeachers();
                });
        } else if ($scope.insertedTeacher && $scope.insertedTeacher.FirstName && $scope.isEdit) {
            $http.put("/Teacher/Edit", $scope.insertedTeacher)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getTeachers();
                });
        }
    };

    $scope.deleteTeacher = function () {
        if ($scope.currentId) {
            $http.delete("/Teacher/Delete?id=" + $scope.currentId)
                .then(() => {
                    $('#deleteModal').modal('hide');

                    $scope.currentId = null;

                    $scope.getTeachers();
                });
        }
    };

    $scope.clearEditValues = function () {
        $scope.isEdit = false;
        $scope.insertedTeacher = {}
    };

    $scope.normalizeDate = function (birthday) {
        var replaced = parseInt(birthday.replace(/([^0-9])+/g, ''));
        var date = new Date(replaced);

        return date;
    };

    $scope.setCurrentId = function (currentId) {
        $scope.currentId = currentId;
    };

    $scope.setTeacherToEdit = function (teacher) {
        $scope.insertedTeacher.FirstName = teacher.FirstName;
        $scope.insertedTeacher.LastName = teacher.LastName;
        $scope.insertedTeacher.Birthday = $scope.normalizeDate(teacher.Birthday);
        $scope.insertedTeacher.Salary = teacher.Salary;
        $scope.insertedTeacher.Id = teacher.Id;

        $scope.isEdit = true;
    };
})
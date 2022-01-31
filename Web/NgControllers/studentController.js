app.controller("StudentController", function ($scope, $http) {
    $scope.insertedSubjectStudent = {}
    $scope.insertedStudent = {};
    $scope.currentId = null;
    $scope.isEdit = false;

    $scope.getValues = function (route, type) {
        $http.get(route)
            .then((res) => {
                switch (type) {
                    case "Subject":
                        $scope.Subjects = res.data;
                        break;
                    case "Student":
                        $scope.Students = res.data;
                        break;
                };

            });
    };

    $scope.insertOrEditStudent = function () {
        if ($scope.insertedStudent && $scope.insertedStudent.FirstName && !$scope.isEdit) {
            $http.post("/Student/Insert", $scope.insertedStudent)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getValues('/Student/GetStudents', 'Student');
                });
        } else if ($scope.insertedStudent && $scope.insertedStudent.FirstName && $scope.isEdit) {
            $http.put("/Student/Edit", $scope.insertedStudent)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getValues('/Student/GetStudents', 'Student');
                });
        }
    };

    $scope.deleteStudent = function () {
        if ($scope.currentId) {
            $http.delete("/Student/Delete?id=" + $scope.currentId)
                .then(() => {
                    $('#deleteModal').modal('hide');

                    $scope.currentId = null;

                    $scope.getValues('/Student/GetStudents', 'Student');
                });
        }
    };

    $scope.enrollStudent = function () {
        $http.post("/Student/EnrollStudent", $scope.insertedSubjectStudent)
            .then(() => {
                $('#enrollModal').modal('hide');

                $scope.insertedSubjectStudent = {};
            });
    }

    $scope.clearEditValues = function () {
        $scope.isEdit = false;
        $scope.insertedStudent = {}
    };

    $scope.normalizeDate = function (birthday) {
        var replaced = parseInt(birthday.replace(/([^0-9])+/g, ''));
        var date = new Date(replaced);

        return date;
    };

    $scope.setCurrentId = function (currentId) {
        $scope.currentId = currentId;
    };

    $scope.setStudentToEdit = function (student) {
        $scope.insertedStudent.FirstName = student.FirstName;
        $scope.insertedStudent.LastName = student.LastName;
        $scope.insertedStudent.RegistrationNumber = student.RegistrationNumber;
        $scope.insertedStudent.Birthday = $scope.normalizeDate(student.Birthday);
        $scope.insertedStudent.Id = student.Id;

        $scope.isEdit = true;
    };
});
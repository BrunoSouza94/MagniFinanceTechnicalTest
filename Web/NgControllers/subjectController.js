app.controller("SubjectController", function ($scope, $http) {
    $scope.insertedSubject = {};
    $scope.currentId = null;
    $scope.isEdit = false;

    $scope.getValues = function (route, type) {
        $http.get(route)
            .then((res) => {
                switch (type) {
                    case "Subject":
                        $scope.Subjects = res.data;
                        break;
                    case "Teacher":
                        $scope.Teachers = res.data;
                        break;
                    case "Course":
                        $scope.Courses = JSON.parse(res.data);
                        break;
                }

            })
    };

    $scope.insertOrEditSubject = function () {
        if ($scope.insertedSubject && $scope.insertedSubject.SubjectName && !$scope.isEdit) {
            console.log($scope.insertedSubject);
            $http.post("/Subject/Insert", $scope.insertedSubject)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getValues('/Subject/GetSubjects', 'Subject');
                });
        } else if ($scope.insertedSubject && $scope.insertedSubject.SubjectName && $scope.isEdit) {
            $http.put("/Subject/Edit", $scope.insertedSubject)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getValues('/Subject/GetSubjects', 'Subject');
                });
        }
    };

    $scope.deleteSubject = function () {
        if ($scope.currentId) {
            $http.delete("/Subject/Delete?id=" + $scope.currentId)
                .then(() => {
                    $('#deleteModal').modal('hide');

                    $scope.currentId = null;

                    $scope.getValues('/Subject/GetSubjects', 'Subject');
                });
        }
    };

    $scope.clearEditValues = function () {
        $scope.isEdit = false;
        $scope.insertedSubject = {}
    };

    $scope.setCurrentId = function (currentId) {
        $scope.currentId = currentId;
    };

    $scope.setSubjectToEdit = function (subject) {
        $scope.insertedSubject.Id = subject.Id;
        $scope.insertedSubject.CourseId = subject.CourseId;
        $scope.insertedSubject.TeacherId = subject.TeacherId;
        $scope.insertedSubject.SubjectName = subject.SubjectName;

        console.log($scope.insertedSubject);

        $scope.isEdit = true;
    };
})
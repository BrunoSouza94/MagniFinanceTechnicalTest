app.controller("SubjectStudentsController", function ($scope, $http) {
    $scope.insertedSubjectStudents = {};
    $scope.currentId = null;

    $scope.getSubjectStudents = function () {
        $http.get("/SubjectStudents/GetSubjectStudents")
            .then((res) => {
                $scope.SubjectStudents = res.data;
            });
    };

    $scope.Edit = function () {
        $http.put("/SubjectStudents/Edit", $scope.insertedSubjectStudents)
            .then(() => {
                $('#editModal').modal('hide');

                $scope.clearEditValues();

                $scope.getSubjectStudents();
            });
    };

    $scope.deleteGrade = function () {
        if ($scope.currentId) {
            $http.delete("/SubjectStudents/Delete?id=" + $scope.currentId)
                .then(() => {
                    $('#deleteModal').modal('hide');

                    $scope.currentId = null;

                    $scope.getSubjectStudents();
                });
        }
    };

    $scope.clearEditValues = function () {
        $scope.isEdit = false;
        $scope.insertedSubjectStudents = {}
    };

    $scope.setCurrentId = function (currentId) {
        $scope.currentId = currentId;
    };

    $scope.setsubjectStudentsToEdit = function (subjectStudents) {
        $scope.insertedSubjectStudents.Id = subjectStudents.Id;
        $scope.insertedSubjectStudents.Grade = subjectStudents.Grade;
        $scope.insertedSubjectStudents.StudentId = subjectStudents.StudentId;
        $scope.insertedSubjectStudents.SubjectId = subjectStudents.SubjectId;

        $scope.isEdit = true;
    };
})
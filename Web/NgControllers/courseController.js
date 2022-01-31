app.controller("CourseController", function ($scope, $http) {
    $scope.insertedCourse = {};
    $scope.currentId = null;
    $scope.isEdit = false;

    $scope.getCourses = function () {
        $http.get("/Course/GetCourses")
            .then((res) => {
                $scope.Courses = JSON.parse(res.data);
                console.log($scope.Courses);
            });
    };

    $scope.insertOrEditCourse = function () {
        if ($scope.insertedCourse && $scope.insertedCourse.CourseName && !$scope.isEdit) {
            $http.post("/Course/Insert", $scope.insertedCourse)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getCourses();
                });
        } else if ($scope.insertedCourse && $scope.insertedCourse.CourseName && $scope.isEdit) {
            $http.put("/Course/Edit", $scope.insertedCourse)
                .then(() => {
                    $('#editModal').modal('hide');

                    $scope.clearEditValues();

                    $scope.getCourses();
                });
        }
    };

    $scope.deleteCourse = function () {
        if ($scope.currentId) {
            $http.delete("/Course/Delete?id=" + $scope.currentId)
                .then(() => {
                    $('#deleteModal').modal('hide');

                    $scope.currentId = null;

                    $scope.getCourses();
                });
        }
    };

    $scope.getTotalStudents = function (subjects) {
        let totalStudents = 0;

        angular.forEach(subjects, (subject) => {
            angular.forEach(subject.Students, () => {
                totalStudents++;
            })
        });

        return totalStudents;
    };

    $scope.clearEditValues = function () {
        $scope.isEdit = false;
        $scope.insertedCourse = {}
    };

    $scope.setCurrentId = function (currentId) {
        $scope.currentId = currentId;
    };

    $scope.setCourseToEdit = function (course) {
        $scope.insertedCourse.CourseName = course.CourseName;
        $scope.insertedCourse.Id = course.Id;

        $scope.isEdit = true;
    };
})
"use strict";

app.controller("blogController", ['$scope', '$location', 'authService', 'blogService', function ($scope, $location, authService, blogService) {
   
    $scope.blogPosts = [];
    $scope.newPost = {};

    $scope.addNewPost = function (input) {
        console.log(input);
        blogService.addPost(input).then(function (response) {
            $scope.blogPosts.push(response.data);
            $scope.newPost = null;
        })
    }

    $scope.getAllBlogPosts = function () {
        blogService.getAllPosts().then(function (response) {
            $scope.blogPosts = response.data;
            console.log(response.data);
        });
    };

    $scope.removePost = function () {
        blogService.removeBlogPost().then(function (post) {
            $scope.blogPosts.remove(post)
        })
    }
}]);
"use strict";

app.service("blogService", function ($http) {
    var addPost = function (blogText) {
        return $http.post("api/blog", blogText);
    }
    var getAllPosts = function () {
        return $http.get("api/blog");
    }
    var removeBlogPost = function () {
        return $http.get("api/blog");
    }
    return {
        addPost: addPost,
        getAllPosts: getAllPosts,
        removeBlogPost: removeBlogPost
    }
})
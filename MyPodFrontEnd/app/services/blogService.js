"use strict";

app.service("blogService", function ($http) {

    var addPost = function (post) {
        var data = post;
        return $http.post("http://localhost:50162/api/blog", data).then(function (results) {
            console.log(results);
        });
    }
    var getAllPosts = function () {
       return $http.get("http://localhost:50162/api/blog").then(function(results){
            return results;
        });
    }
    var removeBlogPost = function () {
        return $http.get("http://localhost:50162/api/blog");
    }
    return {
        addPost: addPost,
        getAllPosts: getAllPosts,
        removeBlogPost: removeBlogPost
    }
})
"use strict";

app.controller("blogController", function (authService, blogService) {
    var vm = this;
    vm.blogPosts = [];
    vm.blogInput;

    vm.addNewPost = function () {
        blogService.addPost(vm.blogInput).then(function () {
            vm.getAllBlogPosts();
        })
    }

    vm.getAllBlogPosts = function () {
        blogService.getAllPosts().then(function (response) {
            vm.blogPosts = response.data;
            console.log(response.data);
        });
    };

    vm.removePost = function () {
        blogService.removeBlogPost().then(function (post) {
            vm.blogPosts.remove(post)
        })
    }
});
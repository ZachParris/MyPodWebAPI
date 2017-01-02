"use strict";

app.controller("blogController", function (blogService) {
    var vm = this;
    vm.blogPosts = [];

    vm.addNewPost = function () {
        blogService.addPost(vm.blogInput).then(function (post) {
            vm.getAllBlogPosts();
        })
    }
    vm.getAllBlogPosts = function () {
        blogService.getAllPosts().then(function (response) {
            vm.blogPosts = response.data
        })
    }
    vm.removePost = function () {
        blogService.removeBlogPost().then(function (post) {
            vm.blogPosts.remove(post)
        })
    }
})
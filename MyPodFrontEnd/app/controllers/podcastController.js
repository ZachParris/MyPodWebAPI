"use strict";

app.controller("podcastController", function (podcastService) {
    var vm = this;
    vm.subscriptionList = [];
    vm.episodes = [];

    vm.getUserSubscriptions = function (url) {
        podcastService.subscriptions(url).then(function (data) {
            console.log("data", data);
            for (let item in data) {
                vm.subscriptionList.push({
                    author: data.author,
                    title: data.title,
                    image: data.imageUrl
                })
            }
        })
    }
    vm.getUserSubscriptions();

  


})
"use strict";

app.controller("podcastController", function (podcastService, searchService) {
    var vm = this;
    const parser = new DOMParser();
    vm.subscriptionList = [];
    vm.episodes = [];

    vm.getUserSubscriptions = function (url) {
        podcastService.subscriptions(url).then(function (response) {
            console.log("data", response.data);
            vm.subscriptionList = response.data;
        }, function (error) {
            debugger
        })
    }
    vm.getUserSubscriptions();

    vm.getEpisodes = function (url) {
        searchService.searchResult(url).then(function (response) {
            vm.episodes = response;
            console.log(response);
            const xml = parser.parseFromString(response.data, "text/xml");
            const enclosures = xml.querySelectorAll("enclosure");
            vm.episodes = [];
            for (let item of enclosures.entries()) {
                var siblings = item[1].parentElement.childNodes;
                for (let key in siblings) {
                    if (siblings[key].nodeName === "title") {
                        vm.episodes.push({
                            url: item[1].attributes.url.nodeValue,
                            title: siblings[key].textContent
                        })
                    }
                }
            }
        }, function (error) {
            debugger
        })
    }

})
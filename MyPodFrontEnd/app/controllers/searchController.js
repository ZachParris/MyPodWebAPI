"use strict";

app.controller("searchController", function (searchService) {
    var vm = this;
    const parser = new DOMParser();
    vm.searchResults = [];
    vm.subscriptions = [];
    vm.episodes = [];

    vm.search = function () {
        searchService.searchItunes(vm.searchInput).then(function (response) {
            vm.searchResults = response.data.results;
        }, function (error) {
            debugger
        })
    }

    vm.getPodcastFeed = function (url) {
        searchService.searchResult(url).then(function (response) {
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

    vm.nowPlaying = function (url) {
        searchService.searchResult(url).then(function (response) {
            getPodcastFeed();
            chosenEpisode = response.data
        })
    }

    vm.followPodcastChannel = function (url) {
        searchService.subscription = function (userChoice) {
            vm.subscriptions = userChoice.response.data;
        }
    }

    vm.unfollowPodcast = function (url) {
        searchService.subscription = function () {

        }
    }

})
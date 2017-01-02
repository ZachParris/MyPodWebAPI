"use strict";

app.service("searchService", function ($http) {
    var searchItunes = function (searchTerm) {
        return $http.get("https://podcast-player-mypod.herokuapp.com/api/iTunes/search?entity=podcast&term=" + searchTerm);
    }
    var searchResult = function (url) {
        return $http.get("https://podcast-player-mypod.herokuapp.com/api/feed/?feedUrl=" + url);
    }
    var subscription = function (channel) {
        return $http.get("https://podcast-player-mypod.herokuapp.com/api/iTunes/search?entity=podcast&term=" + channel)
    }
    return {
        subscription: subscription,
        searchResult: searchResult,
        searchItunes: searchItunes
    }
})
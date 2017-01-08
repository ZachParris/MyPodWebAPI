"use strict";

app.service("searchService", function ($http) {
    var searchItunes = function (searchTerm) {
        return $http.get("https://podcast-player-mypod.herokuapp.com/api/iTunes/search?entity=podcast&term=" + searchTerm);
    }
    var searchResult = function (url) {
        return $http.get("https://podcast-player-mypod.herokuapp.com/api/feed/?feedUrl=" + url);
    }
    var addSubscription = function (channel) {
        var channelObj = {
            feedUrl: channel.feedUrl,
            title: channel.collectionName,
            author: channel.artistName,
            image: channel.artworkUrl600
        }
        console.log(channel)
        return $http.post("http://localhost:50162/api/Podcast", channelObj)
    }
    return {
        addSubscription: addSubscription,
        searchResult: searchResult,
        searchItunes: searchItunes
    }
})
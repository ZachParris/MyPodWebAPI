"use strict";

app.service("podcastService", function ($http) {
    var subscriptions = function () {
        return $http.get("http://localhost:50162/api/Podcast")
    }
    var getAllSubscriptions = function () {
        return $http.get("http://localhost:50162/api/Podcast").then(function (results) {
            return results;
        });
    }
    return {
        subscriptions: subscriptions,
        getAllSubscriptions: getAllSubscriptions
    }

})
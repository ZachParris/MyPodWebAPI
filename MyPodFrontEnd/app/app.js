var app = angular.module('App', ['ngRoute', 'LocalStorageModule']);

app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/", {
        controller: "indexController",
        templateUrl: "app/partials/home.html"
    }).when("/login", {
        controller: "loginController",
        templateUrl: "app/partials/login.html"
    }).when("/signup", {
        controller: "signupController",
        templateUrl: "app/partials/signup.html"
    }).when("/search", {
        controller: "searchController",
        templateUrl: "app/partials/search.html"
    }).when("/blog", {
        controller: "blogController",
        templateUrl: "app/partials/blog.html"
    }).when("/podcasts", {
        controller: "searchController",
        templateUrl: "app/partials/podcasts.html"
    }).otherwise({ redirectTo: "/" });

    $locationProvider.html5Mode(true);
});

app.config(function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
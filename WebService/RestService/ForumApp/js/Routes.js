
var Router = Backbone.Router.extend({
    routes: {
        '': 'home'
    },
    initialize: function () {
        $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
            options.url = 'http://localhost:4546/ForumService.svc' + options.url;
        });
    }
});

var router = new Router();
router.on('route:home', function () {

    var view = new ItemListView();
    
    $("#list_container").append(view.render().el);
    $("table").addClass("table table-condensed");

    App.PostList = view;
});

Backbone.history.start();

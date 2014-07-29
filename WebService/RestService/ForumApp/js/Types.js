
var ForumPost = Backbone.Model.extend({
    urlRoot: '/ForumPost',
    initialize: function () {

    }
});

var ForumPosts = Backbone.Collection.extend({
    url: '/ForumPosts/0',
    model: ForumPost
});

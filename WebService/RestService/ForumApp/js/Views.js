
var ItemView = Backbone.View.extend({
    tagName: "tr",
    template: _.template($("#item-template").html()),
    events: {

    },
    initialize: function () {
        
    },
    render: function () {
        
        var html = this.template(this.model.toJSON());
        this.setElement($(html));
        this.stickit();
        return this;
    },
    bindings: {
        '#description': 'PostDescription'
    },
    events: {
        "click #btnDelete": "deleteRow",
        "click #btnOk": "updateRow"
    },
    deleteRow: function () {
        
        var ID = this.model.get("PostId");
        
        this.model.destroy({
            success: function () {
                App.PostList.render();
            }
        });
    },
    updateRow: function () {
        this.model.save({}, {
            success: function () {
                App.PostList.render();
            }
        });
    }
});

var ItemListView = Backbone.View.extend({
    tagName: 'table',
    initialize: function () {
        
    },
    render: function () {
        var that = this;
        var posts = new ForumPosts();

        posts.fetch({
            success: function (posts) {
                
                var template = _.template($("#list-template").html(), { myposts: posts.models, PostCount: posts.models.length });
                that.$el.html(template);
                
                posts.models.map(function (item) {
                    var view = new ItemView({ model: item });
                    that.$el.append(view.render().$el);
                });
            }
        });
        return this;
    }
    
});
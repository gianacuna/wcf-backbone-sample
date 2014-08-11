using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace RestService
{
    public class ForumService : IForumService
    {
        public List<Post> GetPosts(string id)
        {
            WebServiceEntities dbContext = 
                new WebServiceEntities();
            List<Post> posts = dbContext.Posts
                .Select(p => p).ToList();

            return posts;
        }

        public Post AddPost(Post post)
        {

            post.Timestamp = DateTime.Now;
            post.Username = "sysadmin";

            WebServiceEntities dbContext = 
                new WebServiceEntities();
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            return post;
        }

        public Post DeletePost(string id)
        {
            int ID = Convert.ToInt32(id);

            WebServiceEntities dbContext = 
                new WebServiceEntities();
            
            Post post = dbContext.Posts.Find(ID);

            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();

            return post;
        }

        public Post UpdatePost(string id, Post post)
        {
            WebServiceEntities dbContext = new WebServiceEntities();
            dbContext.Posts.Attach(post);
            dbContext.Entry(post).State = EntityState.Modified;
            dbContext.SaveChanges();

            return post;
        }
    }
}

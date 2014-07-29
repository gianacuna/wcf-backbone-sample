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
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ForumPosts/{id}")]
        public Stream GetPosts(string id)
        {
            WebServiceEntities dbContext = new WebServiceEntities();
            List<Post> posts = dbContext.Posts.Select(p => p).ToList();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonval = js.Serialize(posts);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            return new MemoryStream(Encoding.UTF8.GetBytes(jsonval));
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ForumPost")]
        public Stream AddPost(Post post)
        {
            if (post == null)
            {
                post = new Post() { PostDescription = "", Timestamp = DateTime.Now };
            }

            post.Timestamp = DateTime.Now;
            post.Username = "sysadmin";

            WebServiceEntities dbContext = new WebServiceEntities();
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonval = js.Serialize(post);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            return new MemoryStream(Encoding.UTF8.GetBytes(jsonval));
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ForumPost/{id}")]
        public Stream EditPost(Post post, string id)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(""));
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ForumPost/{id}")]
        public Stream DeletePost(string id)
        {
            int ID = Convert.ToInt32(id);

            WebServiceEntities dbContext = new WebServiceEntities();

            Post post = dbContext.Posts.SingleOrDefault(p => p.PostId == ID);
            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonval = js.Serialize(post);

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            return new MemoryStream(Encoding.UTF8.GetBytes(jsonval));
        }

    }
}

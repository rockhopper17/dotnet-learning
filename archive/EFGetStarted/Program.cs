using System;
using System.Linq;

using var db = new BloggingContext();

// db must be created first
Console.WriteLine($"Database path: {db.DbPath}");

// create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet"});
db.SaveChanges();

// read
Console.WriteLine("Querying for a blog");
var blog  = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!"}
);
db.SaveChanges();

// delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
db.SaveChanges();
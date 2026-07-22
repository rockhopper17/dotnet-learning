using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using EFGetStarted;

Console.WriteLine("hello world");

using var db = new BloggingContext();

// db must be created first:
// > dotnet ef migrations add InitialCreate
// > dotnet ef database update
Console.WriteLine($"db path: {db.DbPath}");

// create
Console.WriteLine("inserting new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

// read
Console.WriteLine("querying for a blog");
var blog = await db.Blogs.OrderBy(b => b.BlogId).FirstAsync();

// update
Console.WriteLine("updating blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(new Post { Title = "hello world", Content = "i write an app with EF Core" });
await db.SaveChangesAsync();

// delete
Console.WriteLine("delete the blog");
db.Remove(blog);
await db.SaveChangesAsync();

Console.WriteLine("done");
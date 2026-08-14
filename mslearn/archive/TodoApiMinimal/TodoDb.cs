using System;
using Microsoft.EntityFrameworkCore;

namespace TodoApi;

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options)
    {
        Console.WriteLine("TodoDb constructor fired");
    }

    public DbSet<Todo> Todos => Set<Todo>();
}

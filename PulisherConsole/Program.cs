﻿// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Diagnostics.Metrics;


PubContext context = new PubContext();
//using (PubContext context = new PubContext()) 
//{
//  context.Database.EnsureCreated();
//}


//AddAuthors();

//GetAuthors();
//AddAuthorWithBook();
//GetAuthorsWithBook();

//QueryFilters();
//SkipAndTakeAuthors();

//QueryAggregate();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthors();
//DeleteAuthor();
//InsertMultipleAuthors();
//GetAuthors();


void GetAuthors() 
{
    //using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors) 
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}
//add authors
 void AddAuthors() 
{
    var author = new Author
    {
        FirstName = "Julie",
        LastName = "Lerman",
    };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

//add author with book
void AddAuthorWithBook() 
{
    var author = new Author
    {
        FirstName = "Lillian",
        LastName = "Wanjiku"
    };
    author.Books.Add(new Book
    {
        Title = "Microsoft Kenya",
        PublishDate = new DateTime(2019, 1, 1)
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Edition",
        PublishDate = new DateTime(2011, 8, 1)
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();

}

void RetrieveAndUpdateAuthor() 
{
    var author = context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName=="Lerman");
    if (author != null)
    {
        author.FirstName = "Julia";
        context.SaveChanges();
    }
    else 
    {
        Console.WriteLine("Author does not exist");
    }
}

void RetrieveAndUpdateMultipleAuthors() 
{
    var LermanAuthors = context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var author in LermanAuthors) 
    {
       author.LastName = "lerman";
    }
    Console.WriteLine("Before" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("After"+ context.ChangeTracker.DebugView.LongView);
    context.SaveChanges();
}

void GetAuthorsWithBook()
{
    //using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors) 
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine("*" + book.Title);
        }
    }
}

void QueryFilters() 
{
    //var name = "Arthur";
    //var authors = context.Authors.Where(s => s.FirstName == name).ToList();

    var filter = "l%";
    var authors = context.Authors
        .Where(a => EF.Functions.Like(a.LastName, filter)).ToList();
}

void SkipAndTakeAuthors() 
{
    var groupSize = 2;
    for (var i = 0; i < 5; i++)
    {
        var authors = context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}");
        foreach (var author in authors) 
        {
            Console.WriteLine($"{author.FirstName} {author.LastName}");
        }
    }
}

void sortAuthors () 
{
    var authorsByLastName = context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName)
        .ToList();
    authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));
}

void QueryAggregate()
{
    var author = context.Authors.OrderByDescending(a => a.FirstName)
         .FirstOrDefault(a => a.LastName == "Wanjiku");
}

void DeleteAuthor() 
{
    var extraJL = context.Authors.Find(10);
    if (extraJL != null) 
    {
        context.Authors.Remove(extraJL);
        context.SaveChanges();
    }
}

void InsertMultipleAuthors()
{
    context.Authors.AddRange(new Author { FirstName ="Ruth", LastName="Ozeki"},
        new Author { FirstName = "Sofia", LastName = "Segovia" },
        new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
        new Author { FirstName = "Hugh", LastName = "Howey" },
        new Author { FirstName = "Isabelle", LastName = "Allende" });
    context.SaveChanges();
}

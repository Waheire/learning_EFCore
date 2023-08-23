// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Diagnostics.Metrics;

using (PubContext context = new PubContext()) 
{
    context.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthors();
//GetAuthors();
AddAuthorWithBook();
GetAuthorsWithBook();



void GetAuthors() 
{
    using var context = new PubContext();
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
        FirstName = "Rahab",
        LastName = "Wanjiku",
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

void GetAuthorsWithBook()
{
    using var context = new PubContext();
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

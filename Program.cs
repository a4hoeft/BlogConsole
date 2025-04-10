using System.Diagnostics;
using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
 var db = new DataContext();
 
logger.Info("Program started");

// Display menu options
Console.WriteLine("Menu Options:");
Console.WriteLine("1. Add a new Blog");
Console.WriteLine("2. Display all Blogs");
Console.WriteLine("3. Create Post");
Console.WriteLine("4. Display Posts");
Console.WriteLine("5. Exit");
string userInput = Console.ReadLine();

switch (userInput)
{
    case "1":
        Console.Write("Enter a name for a new Blog: ");
        var blogName = Console.ReadLine();

        var newBlog = new Blog { Name = blogName };

       
        db.AddBlog(newBlog);
        logger.Info("Blog added - {name}", blogName);
        break;

    case "2":
        // Display all Blogs from the database
        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine("All blogs in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
        logger.Info("Displayed all blogs");
        break;

    case "3":
        // Code for creating a post
        Console.WriteLine("Create Post functionality not implemented yet.");
        break;

    case "4":
        // Code for displaying posts
        Console.WriteLine("Display Posts functionality not implemented yet.");
        break;

    case "5":
        logger.Info("Program ended");
        return;

    default:
        Console.WriteLine("Invalid option. Please try again.");
        break;
}





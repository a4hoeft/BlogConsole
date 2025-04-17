using System.Diagnostics;
using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";



///refactor code to use les repeated code
/// check and fix bugs
/// add formating to make output more readable


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
string userInput = Console.ReadLine(); //validation on user input 

switch (userInput)
{
    case "1":
        Console.Write("Enter a name for a new Blog: ");
        var blogName = Console.ReadLine();

        var newBlog = new Blog { Name = blogName };

        db.AddBlog(newBlog);
        Console.WriteLine($"Blog '{blogName}' added successfully."); //verification for the user
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
        //better spacing
        logger.Info("Displayed all blogs");
        break;

    case "3":
    // Code for creating a post
    Console.WriteLine("Select a Blog to create a Post:");
    foreach (var blog in db.Blogs.OrderBy(b => b.Name)) // Display all blogs
    {
        Console.WriteLine($"{blog.BlogId}. {blog.Name}");
    }
    Console.Write("Enter the Blog ID: ");
    var blogIdInput = Console.ReadLine();

    if (int.TryParse(blogIdInput, out int blogId))
    {
        // Check if the Blog ID exists in the database
        var selectedBlog = db.Blogs.FirstOrDefault(b => b.BlogId == blogId);
        if (selectedBlog != null)
        {
            Console.Write("Enter the Post Title: ");
            var postTitle = Console.ReadLine();
            Console.Write("Enter the Post Content: ");
            var postContent = Console.ReadLine();

            var newPost = new Post
            {
                Title = postTitle,
                Content = postContent,
                BlogId = blogId
            };

            db.Posts.Add(newPost);
            db.SaveChanges();
            Console.WriteLine($"Post '{postTitle}' created successfully.");
            logger.Info("Post created - {title}", postTitle);
        }
        else
        {
            Console.WriteLine("Invalid Blog ID. No blog found with the given ID.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid numeric Blog ID.");
    }
    break;
     
    case "4":
        // Code for displaying posts
        Console.WriteLine("Select a Blog to display Posts:");
        foreach (var blog in db.Blogs.OrderBy(b => b.Name)) 
        {
            Console.WriteLine($"{blog.BlogId}. {blog.Name}");
        }
        Console.Write("Enter the Blog ID: ");
        var displayBlogIdInput = Console.ReadLine();
        if (int.TryParse(displayBlogIdInput, out int displayBlogId))
        {
            var posts = db.Posts.Where(p => p.BlogId == displayBlogId).OrderBy(p => p.Title);
            Console.WriteLine($"Posts for Blog ID {displayBlogId}:");
            foreach (var post in posts)
            {
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Content: {post.Content}");
                Console.WriteLine();
            }
            logger.Info("Displayed posts for Blog ID - {blogId}", displayBlogId);
        }
        else
        {
            Console.WriteLine("Invalid Blog ID.");
        }
        break;

    case "5":
        logger.Info("Program ended");
        return;

    default:
        Console.WriteLine("Invalid option. Please try again.");
        break;
}





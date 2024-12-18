using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsBlog.Models;

namespace NewsBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.Blog.Where(b => b.IsPublish).Take(10).OrderByDescending(x => x.CreateTime).ToList();
        foreach (var blog in list)
        {
            blog.Author = _context.Author.Find(blog.AuthorId);
        }
        return View(list);
    }
    
    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Contact()
    {
        return View();
    }
    
    public IActionResult Post(int id)
    {
        var blog = _context.Blog.Find(id);
        blog.Author = _context.Author.Find(blog.AuthorId);
        blog.ImagePath = "/img/" + blog.ImagePath;
        return View(blog);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
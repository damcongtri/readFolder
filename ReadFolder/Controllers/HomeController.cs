using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReadFolder.Models;

namespace ReadFolder.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult DisplayFileSystemEntries(string directoryPath)
    {
        try
        {
            FileSystemModel root = new FileSystemModel();
            root.Name = directoryPath;
            root.IsFile = false;
            root.Children = GetFileSystemEntries(directoryPath);

            ViewBag.FileSystemTree = root;
        }
        catch (Exception e)
        {
            ViewBag.Error = "An error occurred: " + e.Message;
        }

        return View("Index");
    }

    private List<FileSystemModel> GetFileSystemEntries(string path)
    {
        List<FileSystemModel> entries = new List<FileSystemModel>();

        string[] directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            FileSystemModel item = new FileSystemModel();
            item.Name ="[Folder]" + Path.GetFileName(directory);
            item.IsFile = false;
            item.Children = GetFileSystemEntries(directory);
            entries.Add(item);
        }

        string[] files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            FileSystemModel item = new FileSystemModel();
            item.Name ="[File]" +Path.GetFileName(file);
            item.IsFile = true;
            entries.Add(item);
        }

        return entries;
    }
    
    
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
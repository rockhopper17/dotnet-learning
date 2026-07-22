using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class  HelloWorldController : Controller
{
    // GET: /HelloWorld/
    public string Index()
    {
        return "default action";
    }

    // GET: /HelloWorld/Welcome
    public string Welcome(string name, int ID = 1)
    {
        // return "welcome method";
        return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
    }
    // public string Welcome(string name, int numTimes = 1)
    // {
    //     // return "welcome method";
    //     return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
    // }
}
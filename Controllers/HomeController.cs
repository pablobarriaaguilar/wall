using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using wall.Models;

namespace wall.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context; 
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;    
    }


    public IActionResult Index()
    {
        var model = new LoginUserViewModel
    {
        Usuario = new Usuario(), // Initialize Usuario
        LoginUser = new LoginUser() // Initialize LoginUser
    };
        return View(model);
    }

     [HttpPost("user/create")]
    public IActionResult CreateUser(Usuario _user){
    LoginUserViewModel MyModels = new LoginUserViewModel();
        MyModels.Usuario = _user;
        if (ModelState.IsValid){
            Console.WriteLine("es valido");
            PasswordHasher<Usuario> Hasher = new PasswordHasher<Usuario>();   
            _user.Password = Hasher.HashPassword(_user, _user.Password); 
            _context.Add(_user);
            _context.SaveChanges();
           
            return RedirectToAction("Index");
        }else{
            Console.WriteLine("Algo salió mal. Mensajes de error:");
        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
            return View("Index", MyModels);
        }
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginUser userSubmission) //parameter to pass in, the user submission of their email and password
    {
        //EMAIL CHECK
        //Check if submission is valid according to our model
        if (!ModelState.IsValid) //If submission is not valid according to our model
        {
            return View("Index"); //return them back to the index view to register/login again
        }

        //Check if email submitted is in our database
        Usuario? userInDb = _context.Users.FirstOrDefault(e => e.Email == userSubmission.Email);

        //If nothing comes back, add an error and return the same view to render the validation
        if (userInDb == null)
        {
            ModelState.AddModelError("Email", "Invalid Email Address/Password");
            return View("Index");
        }

        //PASSWORD CHECK
        //Invoke password hasher
        PasswordHasher<LoginUser> hashbrowns = new PasswordHasher<LoginUser>();
        //LoginUser, hashed password, password submitted by user
        var result = hashbrowns.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
        

        if (result == 0)
        {
            ModelState.AddModelError("Email", "Invalid Email Address/Password"); //string key, error message parameters
            return View("Index");
        }

        //PASSES ALL CHECKS, HANDLE SUCCESS NOW
        //We want to set the session key:value pair to id:UserId and we'll use this to keep track if our user is logged in
        HttpContext.Session.SetInt32("UUID", userInDb.UsuarioId);
        HttpContext.Session.SetString("UName", userInDb.FirstName);
        return RedirectToAction("Index", "Message");

    }


public IActionResult Logout(){
    HttpContext.Session.Clear();
    return RedirectToAction("Index");
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

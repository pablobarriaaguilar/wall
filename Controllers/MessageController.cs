using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wall.Models;

namespace wall.Controllers;

public class MessageController : Controller
{
    private readonly ILogger<MessageController> _logger;
    private MyContext _context; 
    public MessageController(ILogger<MessageController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;    
    }


    public IActionResult Index()
    {   
       
       

      List<Usuario> UsuarioMensaje = _context.Users.Include(m => m.AllMessages).ToList();
var algo = _context.Messages.Include(u => u.Comments).Include(u => u.Creator ).ToList();
List<MessageViewModel> ListaMensajes = new List<MessageViewModel>();
foreach(var msj in algo ){
    MessageViewModel m = new MessageViewModel();
    m.MessageId = msj.MessageId;
    m.MessageText = msj.MessageText;
    m.Comments = msj.Comments;
    m.Usuario = msj.Creator;
    m.CreatedAt = msj.CreatedAt;
    ListaMensajes.Add(m);
}

    /*List <MessageViewModel> _MessageViewModel = new List <MessageViewModel>();
   

    foreach( Message me in _Message){
         List <CommentViewModel> _CommentViewModel = new List<CommentViewModel>();
        MessageViewModel _me = new MessageViewModel();
        _me.MessageId = me.MessageId;
        _me.MessageText = me.MessageText;
        foreach( Comment com in me.MessageComment){
            CommentViewModel _com = new CommentViewModel();
            _com.CommentId = com.CommentId;
            _com.CommentText = com.CommentText;
            _com.UserName = com.Usuario.FirstName;
            _CommentViewModel.Add(_com);

        }@await Html.PartialAsync("_Createmessage")
        _me.Comments = _CommentViewModel;
        _MessageViewModel.Add(_me);
    }*/

        ViewBag.ListaMensajes = ListaMensajes;

             var model = new MessageCommentViewModel
    {
        Message = new Message(), // Initialize Usuario
        Comment = new Comment() // Initialize LoginUser
    };

        return View(model);
    }
[HttpPost]
[Route("message/create")]
    public IActionResult Create(Message _Message){
        if(ModelState.IsValid){
            Console.WriteLine("algo salio BIEN!!!!!!!!!!!!!");
           int UUID = (int) HttpContext.Session.GetInt32("UUID");
           _Message.UsuarioId = UUID;
           _context.Messages.Add(_Message);
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
            Console.WriteLine("algo salio mal");
               List<Usuario> UsuarioMensaje = _context.Users.Include(m => m.AllMessages).ToList();
var algo = _context.Messages.Include(u => u.Comments).Include(u => u.Creator ).ToList();
List<MessageViewModel> ListaMensajes = new List<MessageViewModel>();
foreach(var msj in algo ){
    MessageViewModel m = new MessageViewModel();
    m.MessageId = msj.MessageId;
    m.MessageText = msj.MessageText;
    m.Comments = msj.Comments;
    m.Usuario = msj.Creator;
    m.CreatedAt = msj.CreatedAt;
    ListaMensajes.Add(m);
}


        ViewBag.ListaMensajes = ListaMensajes;

             var model = new MessageCommentViewModel
    {
        Message = new Message(), // Initialize Usuario
        Comment = new Comment() // Initialize LoginUser
    };
     return View("Index",model);
        }
        
    }


public IActionResult CommentCreate(Comment _Comment){
    
    int UUID = (int) HttpContext.Session.GetInt32("UUID");

    _Comment.UsuarioId=UUID;
    Console.WriteLine("EL MENSAJE id es !!!"+_Comment.MessageId);
    Console.WriteLine(" el mensaje enviado es *********" + _Comment.CommentText);
    Console.WriteLine(ModelState.IsValid);

    if (!ModelState.IsValid)
{
    foreach (var modelStateEntry in ModelState.Values)
    {
        foreach (var error in modelStateEntry.Errors)
        {
            Console.WriteLine("Error: " + error.ErrorMessage);
        }
    }
}
    if(ModelState.IsValid){
        _context.Add(_Comment);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }else{
              List<Usuario> UsuarioMensaje = _context.Users.Include(m => m.AllMessages).ToList();
var algo = _context.Messages.Include(u => u.Comments).Include(u => u.Creator ).ToList();
List<MessageViewModel> ListaMensajes = new List<MessageViewModel>();
foreach(var msj in algo ){
    MessageViewModel m = new MessageViewModel();
    m.MessageId = msj.MessageId;
    m.MessageText = msj.MessageText;
    m.Comments = msj.Comments;
    m.Usuario = msj.Creator;
    m.CreatedAt = msj.CreatedAt;
    ListaMensajes.Add(m);
}
 ViewBag.ListaMensajes = ListaMensajes;
           var model = new MessageCommentViewModel
    {
        Message = new Message(), // Initialize Usuario
        Comment = new Comment() // Initialize LoginUser
    };


        MessageCommentViewModel messageCommentViewModel = new MessageCommentViewModel();
        messageCommentViewModel.Comment =_Comment;
        return View("Index",model);
    }
    
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

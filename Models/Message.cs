using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;
#pragma warning disable CS8618

namespace wall.Models;
public class Message{

    [Key]
    public int MessageId{ get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    public string MessageText { get; set;}
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [ForeignKey("UsuarioId")] 
    public int UsuarioId { get; set; }
    public Usuario? Creator { get; set; }

    
    public List<Comment> ? Comments { get; set; }
}
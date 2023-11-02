using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace wall.Models;
public class Comment{

    [Key]
    public int CommentId{ get; set; }
    [Required]
    public string CommentText{ get; set; }
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [ForeignKey("MessageId")]
    public int MessageId{ get; set; }
    [ForeignKey("UsuarioId")]
    public int UsuarioId { get; set; }

    public Message? Message { get; set; }
    public Usuario? Usuario { get; set; }
}
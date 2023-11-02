using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace wall.Models;

  public class MessageViewModel
{
    public int MessageId { get; set; }
    public Usuario? Usuario { get; set; }
    public string MessageText { get; set; }
    public List<Comment> Comments { get; set; }

    public Message ? Message { get; set; }
     public DateTime  CreatedAt  = new DateTime();
}
    
    

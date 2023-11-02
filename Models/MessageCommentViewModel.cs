using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace wall.Models;

  public class MessageCommentViewModel
{
    public Message ? Message {get; set;}
    public Comment Comment {get; set; }

    public int ? MessageId{get; set;}
}
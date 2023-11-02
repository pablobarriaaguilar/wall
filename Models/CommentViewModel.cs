using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace wall.Models;

  public class CommentViewModel
{
    public int CommentId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }

}
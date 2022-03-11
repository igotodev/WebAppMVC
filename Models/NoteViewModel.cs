using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class NoteViewModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Message { get; set; }

    public DateTime CreatedDataTime { get; set; } = DateTime.Now;
}
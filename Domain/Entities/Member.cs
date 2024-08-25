using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Member
{

    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
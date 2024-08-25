namespace Application.DTOs;

public class MembersDto
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
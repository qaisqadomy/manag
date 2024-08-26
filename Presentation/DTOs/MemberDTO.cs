using System;

namespace Application.DTOs;

public class MemberDto
{

    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }

}

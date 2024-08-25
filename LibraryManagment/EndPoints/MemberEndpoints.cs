using Application.DTOs;
using Application.IServices;

namespace LibraryManagment.EndPoints;

public static class MemberEndpoints
{
    public static void MapMemberEndpoints(this WebApplication app)
    {
        var membersGroup = app.MapGroup("/members")
            .WithTags("Members");
        membersGroup.MapGet("/GetAll", (IMemberService memberService) =>
        {
            try
            {
                List<MembersDto> list = memberService.GetAll();
                return Results.Ok(list);
            }
            catch (Exception e)
            {
                return Results.NotFound(new { e.Message });
            }
        });
        membersGroup.MapGet("/Get{id}", (int memberId, IMemberService memberService) =>
        {
            try
            {
                MembersDto member = memberService.Find(memberId);
                return Results.Ok(member);
            }
            catch (Exception e)
            {
                return Results.NotFound(new { e.Message });
            }
        });
        membersGroup.MapPost("/add", (MembersDto model, IMemberService memberService) =>
        {
            try
            {
                memberService.Add(model);
                return Results.Ok("added successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(new { e.Message });
            }
        });
        membersGroup.MapDelete("/remove", (int memberId, IMemberService memberService) =>
        {
            try
            {
                memberService.Remove(memberId);
                return Results.Ok("removed successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(new { e.Message });
            }
        });
        membersGroup.MapPut("/update", (MembersDto model, IMemberService memberService) =>
        {
            try
            {
                memberService.Update(model);
                return Results.Ok("updated successfully");
            }
            catch (Exception e)
            {
                return Results.NotFound(new { e.Message });
            }
        });
    }
}
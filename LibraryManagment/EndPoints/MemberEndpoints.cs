using Application.DTOs;
using Application.Services;

namespace LibraryManagment.EndPoints;

public static class MemberEndpoints
{
    public static void MapMemberEndpoints(this WebApplication app)
    {
        var membersGroup = app.MapGroup("/members")
            .WithTags("Members");
        membersGroup.MapGet("/GetAll", (MemberService memberService) =>
        {
            try
            {
                List<MembersDto> list = memberService.GetAll();
                return Results.Ok(list);
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        membersGroup.MapGet("/Get{id}", (int memberId, MemberService memberService) =>
        {
            try
            {
                MembersDto member = memberService.Find(memberId);
                return Results.Ok(member);
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        });
        membersGroup.MapPost("/add", (MembersDto model, MemberService memberService) =>
        {
            try
            {
                memberService.Add(model);
                return Results.Ok("added successfully");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        membersGroup.MapDelete("/remove", (int memberId, MemberService memberService) =>
        {
            try
            {
                memberService.Remove(memberId);
                return Results.Ok("removed successfully");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        membersGroup.MapPut("/update", (MembersDto model, MemberService memberService) =>
        {
            try
            {
                memberService.Update(model);
                return Results.Ok("updated successfully");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}
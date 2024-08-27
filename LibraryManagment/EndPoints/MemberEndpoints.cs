using Application.Services;
using Application.DTOs;

namespace LibraryManagment.EndPoints;

public static class MemberEndpoints
{
        public static void MapMemberEndpoints(this WebApplication app)
        {
        RouteGroupBuilder membersGroup = app.MapGroup("/members")
                    .WithTags("Members");
                membersGroup.MapGet("/", (MemberService memberService) =>
                {
                        List<MemberDTO> list = memberService.GetAll();
                        return Results.Ok(list);
                });
                membersGroup.MapGet("/{id}", (int memberId, MemberService memberService) =>
                {
                        MemberDTO member = memberService.Find(memberId);
                        return Results.Ok(member);
                });
                membersGroup.MapPost("/", (MemberDtoCreate model, MemberService memberService) =>
                {
                        memberService.Add(model);
                        return Results.Created();
                });
                membersGroup.MapDelete("/", (int memberId, MemberService memberService) =>
                {
                        memberService.Remove(memberId);
                        return Results.NoContent;
                });
                membersGroup.MapPut("/{id}", (MemberDtoCreate model,int memberId, MemberService memberService) =>
                {
                        memberService.Update(model, memberId);
                        return Results.Ok();
                });
        }
}
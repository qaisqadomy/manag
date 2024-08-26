using Application.Services;
using Application.DTOs;

namespace LibraryManagment.EndPoints;

public static class MemberEndpoints
{
        public static void MapMemberEndpoints(this WebApplication app)
        {
                var membersGroup = app.MapGroup("/members")
                    .WithTags("Members");
                membersGroup.MapGet("/", (MemberService memberService) =>
                {
                        List<MemberDto> list = memberService.GetAll();
                        return Results.Ok(list);
                });
                membersGroup.MapGet("/{id}", (int memberId, MemberService memberService) =>
                {
                        MemberDto member = memberService.Find(memberId);
                        return Results.Ok(member);
                });
                membersGroup.MapPost("/", (MemberDtoUpdate model, MemberService memberService) =>
                {
                        memberService.Add(model);
                        return Results.Created();
                });
                membersGroup.MapDelete("/", (int memberId, MemberService memberService) =>
                {
                        memberService.Remove(memberId);
                        return Results.NoContent;
                });
                membersGroup.MapPut("/", (MemberDtoUpdate model, MemberService memberService) =>
                {
                        memberService.Update(model);
                        return Results.Ok();
                });
        }
}
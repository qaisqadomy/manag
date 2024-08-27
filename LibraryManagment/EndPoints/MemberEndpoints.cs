using Application.Services;
using Application.DTOs;
using LibraryManagment.Validators;

namespace LibraryManagment.EndPoints;

public static class MemberEndpoints
{
        public static void MapMemberEndpoints(this WebApplication app)
        {

                MemberDtoCreateValidator validationRules = new MemberDtoCreateValidator();

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
                        if (!validationRules.Validate(model).IsValid)
                        {
                                return Results.BadRequest();
                        }
                        memberService.Add(model);
                        return Results.Created();
                });
                membersGroup.MapDelete("/", (int memberId, MemberService memberService) =>
                {
                        memberService.Remove(memberId);
                         return Results.Ok();
                });
                membersGroup.MapPut("/{id}", (MemberDtoCreate model, int memberId, MemberService memberService) =>
                {
                        if (!validationRules.Validate(model).IsValid)
                        {
                                return Results.BadRequest();
                        }
                        memberService.Update(model, memberId);
                        return Results.Ok();
                });
        }
}
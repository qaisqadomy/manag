using Application.DTOs;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class MemberService(IMemberRepo repo)
{
    public void Add(MemberDtoUpdate model)
    {
        Member member = new Member
        {
            Name = model.Name,
            Email = model.Email
        };
        repo.Add(member);
    }

    public List<MemberDto> GetAll()
    {
        List<Member> members = repo.GetAll();
        return members.ConvertAll(member => new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email
        }).ToList();
    }

    public void Remove(int memberId)
    {
        repo.Remove(memberId);
    }

    public void Update(MemberDtoUpdate model)
    {
        Member member = new Member
        {
            Name = model.Name,
            Email = model.Email
        };
        repo.Update(member);
    }

    public MemberDto Find(int memberId)
    {
        Member member = repo.Find(memberId)!;

        MemberDto memberDto = new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email
        };
        return memberDto;
    }
}
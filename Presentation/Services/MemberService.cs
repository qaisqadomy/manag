using Application.DTOs;
using Application.IServices;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace Application.Services;

public class MemberService(IMemberRepo repo) : IMemberService
{
    public void Add(MembersDto model)
    {
        Member member = new Member
        {
            Name = model.Name,
            Email = model.Email
        };
        repo.Add(member);
    }

    public List<MembersDto> GetAll()
    {
        List<Member> members = repo.GetAll();
        return members.ConvertAll(member => new MembersDto
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

    public void Update(MembersDto model)
    {
        Member member = new Member
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email
        };
        repo.Update(member);
    }

    public MembersDto Find(int memberId)
    {
        Member member = repo.Find(memberId)!;
        if (member == null) throw new NotFound("member not found");
        MembersDto memberDto = new MembersDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email
        };
        return memberDto;
    }
}
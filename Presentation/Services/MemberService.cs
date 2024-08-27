using Application.DTOs;
using Domain.Entities;
using Domain.IRepo;

namespace Application.Services;

public class MemberService(IMemberRepo repo)
{
    public void Add(MemberDtoCreate model)
    {
        Member member = new Member
        {
            Name = model.Name,
            Email = model.Email
        };
        repo.Add(member);
    }

    public List<MemberDTO> GetAll()
    {
        List<Member> members = repo.GetAll();
        return members.ConvertAll(member => new MemberDTO
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

    public void Update(MemberDtoCreate model ,int memberId)
    {
        Member member = new Member
        {
            Name = model.Name,
            Email = model.Email
        };
        repo.Update(member,memberId);
        
    }

    public MemberDTO Find(int memberId)
    {
        Member member = repo.Find(memberId)!;

        MemberDTO memberDto = new MemberDTO
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email
        };
        return memberDto;
    }
}
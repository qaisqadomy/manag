using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;
using Infrastructure;

namespace Application.Repos;

public class MemberRepo(AppDbContext context) : IMemberRepo
{
    public void Add(Member member)
    {
        if (context.Members.Any(b => b.Name == member.Name && b.Email == member.Email))
            throw new MemberExist("Member already exist");
        context.Members.Add(member);
        context.SaveChanges();
    }

    public List<Member> GetAll()
    {
        return context.Members.ToList();
    }

    public void Remove(int memberId)
    {
        Member m = context.Members.Find(memberId) ?? throw new MemberNotFound("Member not found");
        context.Members.Remove(m);
        context.SaveChanges();
    }

    public void Update(Member member)
    {
        Member m = context.Members.Find(member.Id) ?? throw new MemberNotFound("Member not found");
        m.Name = member.Name;
        m.Email = member.Email;
        context.Entry(m).CurrentValues.SetValues(member);
        context.SaveChanges();
    }

    public Member Find(int memberId)
    {
        Member member = context.Members.FirstOrDefault(m => m.Id == memberId)!;
        if (member == null) throw new MemberNotFound("doesnt exist");
        return member;
    }
}
using Domain.Entities;

namespace Domain.IRepo;

public interface IMemberRepo
{
    List<Member> GetAll();
        void Add(Member member);
    void Remove(int memberId);
    void Update(Member member,int memberId);
    Member? Find(int memberId);
}
using Application.DTOs;

namespace Application.IServices;

public interface IMemberService
{
    void Add(MembersDto model);
    List<MembersDto> GetAll();
    void Remove(int memberId);
    void Update(MembersDto model);
    MembersDto Find(int memberId);
}
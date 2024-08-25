using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;
using Moq;

namespace TestProject1;

public class MemberServiceTests
{
    private readonly Mock<IMemberRepo> _mockRepo;
    private readonly MemberService _service;

    public MemberServiceTests()
    {
        _mockRepo = new Mock<IMemberRepo>();
        _service = new MemberService(_mockRepo.Object);
    }

    [Fact]
    public void Add_ShouldCallRepoAddOnce()
    {
        var memberDto = new MembersDto { Name = "qais", Email = "qais@gmail.com" };
            
        _service.Add(memberDto);
            
        _mockRepo.Verify(r => r.Add(It.Is<Member>(m =>
            m.Id == memberDto.Id &&
            m.Name == memberDto.Name &&
            m.Email == memberDto.Email)), Times.Once);
    }

    [Fact]
    public void GetAll_ShouldReturnListOfMembersDto()
    {
        var members = new List<Member>
        {
            new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" },
            new Member { Id = 2, Name = "ali", Email = "ali@gmail.com" }
        };

        _mockRepo.Setup(r => r.GetAll()).Returns(members);
            
        var result = _service.GetAll();
            
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, m => m is { Id: 1, Name: "qais", Email: "qais@gmail.com" });
        Assert.Contains(result, m => m is { Id: 2, Name: "ali", Email: "ali@gmail.com" });
    }

    [Fact]
    public void Remove_ShouldCallRepoRemoveOnce()
    {
        const int id = 1;
            
        _service.Remove(id);
            
        _mockRepo.Verify(r => r.Remove(id), Times.Once);
    }

    [Fact]
    public void Update_ShouldCallRepoUpdateOnce()
    {
        var memberDto = new MembersDto { Name = "qais", Email = "qais@gmail.com" };
            
        _service.Update(memberDto);
            
        _mockRepo.Verify(r => r.Update(It.Is<Member>(m =>
            m.Name == memberDto.Name &&
            m.Email == memberDto.Email)), Times.Once);
    }

    [Fact]
    public void Find_ShouldReturnMemberDto_WhenMemberExists()
    {
        var member = new Member {  Name = "qais", Email = "qais@gmail.com" };
        _mockRepo.Setup(r => r.Find(1)).Returns(member);
            
        var result = _service.Find(1);
            
        Assert.NotNull(result);
        Assert.Equal("qais", result.Name);
        Assert.Equal("qais@gmail.com", result.Email);
    }

    [Fact]
    public void Find_ShouldThrowNotFoundException_WhenMemberDoesNotExist()
    {
        _mockRepo.Setup(r => r.Find(1)).Returns((Member)null!);
        Assert.Throws<NotFound>(() => _service.Find(1));
    }
}
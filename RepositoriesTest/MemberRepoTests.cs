using Application.Repos;
using Domain.Entities;
using Domain.Exeptions;

namespace RepositoriesTest
{
    public class MemberRepoTests 
    {
        private readonly InMemoryDb _inMemoryDb;
        private readonly MemberRepo _memberRepo;

        public MemberRepoTests()
        {
            _inMemoryDb = new InMemoryDb();
            _memberRepo = new MemberRepo(_inMemoryDb.DbContext);
        }
        

        [Fact]
        public void Add_ShouldThrowInvalidOperation_WhenMemberExists()
        {
            Member existingMember = TestData.ExistingMember;
            _inMemoryDb.DbContext.Members.Add(existingMember);
            _inMemoryDb.DbContext.SaveChanges();

            Assert.Throws<MemberExist>(() => _memberRepo.Add(existingMember));
        }

        [Fact]
        public void Add_ShouldAddMember_WhenMemberDoesntExist()
        {
            Member newMember = TestData.CreateNewMember;
            _memberRepo.Add(newMember);


            Member addedMember = _inMemoryDb.DbContext.Members
                .SingleOrDefault(m => m.Id == newMember.Id)!;

            Assert.NotNull(addedMember);
            Assert.Equal(newMember.Name, addedMember.Name);
            Assert.Equal(newMember.Email, addedMember.Email);
        }

        [Fact]
        public void Remove_ShouldThrowNotFound_WhenMemberDoesntExist()
        {
            Assert.Throws<MemberNotFound>(() => _memberRepo.Remove(5));
        }

        [Fact]
        public void Remove_ShouldRemoveMember_WhenMemberExists()
        {
            Member member = TestData.CreateNewMember;
            _inMemoryDb.DbContext.Members.Add(member);
            _inMemoryDb.DbContext.SaveChanges();

            _memberRepo.Remove(1);

            Member removedMember = _inMemoryDb.DbContext.Members.SingleOrDefault(m => m.Id == 1)!;
            Assert.Null(removedMember);
        }

        [Fact]
        public void Update_ShouldThrowNotFound_WhenMemberDoesNotExist()
        {
            Member updatedMember = TestData.CreateNewMember;

            Assert.Throws<MemberNotFound>(() => _memberRepo.Update(updatedMember,1));
        }

        [Fact]
        public void Update_ShouldUpdateMember_WhenMemberExists()
        {
            Member member = TestData.CreateNewMember;
            _inMemoryDb.DbContext.Members.Add(member);
            _inMemoryDb.DbContext.SaveChanges();

            Member updatedMember = TestData.UpdatedMember;
            _memberRepo.Update(updatedMember,1);

            Member memberInDb = _inMemoryDb.DbContext.Members.SingleOrDefault(m => m.Id == 1)!;
            Assert.NotNull(memberInDb);
            Assert.Equal("ahmad", memberInDb.Name);
            Assert.Equal("ahmad@gmail.com", memberInDb.Email);
        }

        [Fact]
        public void Find_ShouldThrowNotFound_WhenMemberDoesntExist()
        {
            Assert.Throws<MemberNotFound>(() => _memberRepo.Find(1));
        }

        [Fact]
        public void Find_ShouldReturnMember_WhenMemberExists()
        {
            Member member = TestData.CreateNewMember;
            _inMemoryDb.DbContext.Members.Add(member);
            _inMemoryDb.DbContext.SaveChanges();

            Member result = _memberRepo.Find(1);

            Assert.Equal(member.Id, result.Id);
            Assert.Equal(member.Name, result.Name);
            Assert.Equal(member.Email, result.Email);
        }


        [Fact]
        public void GetAll_ShouldReturnAllMembers()
        {
            List<Member> members = TestData.CreateMultipleMembers;
            
            _inMemoryDb.DbContext.Members.AddRange(members);
            _inMemoryDb.DbContext.SaveChanges();

            List<Member> retrievedMembers = _memberRepo.GetAll();
            Assert.NotNull(retrievedMembers);
            Assert.Equal(members.Count, retrievedMembers.Count);
            Assert.Contains(retrievedMembers, m => m.Id == 1);
            Assert.Contains(retrievedMembers, m => m.Id == 2);
        }
    }
}
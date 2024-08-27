using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;
using Moq;

namespace TestProject1;

public class LibraryServiceTests
{
    private readonly Mock<ILibraryRepo> _libraryRepoMock;
    private readonly Mock<IMemberRepo> _memberRepoMock;
    private readonly Mock<IBookRepo> _bookRepoMock;
    private readonly LibraryService _libraryService;

    public LibraryServiceTests()
    {
        _libraryRepoMock = new Mock<ILibraryRepo>();
        _memberRepoMock = new Mock<IMemberRepo>();
        _bookRepoMock = new Mock<IBookRepo>();
        _libraryService = new LibraryService(
            _libraryRepoMock.Object,
            _memberRepoMock.Object,
            _bookRepoMock.Object
        );
    }

    [Fact]
    public void BorrowBook_ValidInputs_ShouldCallBorrowBookOnLibraryRepo()
    {
        List<Book> books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        List<Member> members = new List<Member> { new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        _memberRepoMock.Setup(repo => repo.GetAll())
            .Returns(members);
        _libraryService.BorrowBook(1, 1);
        _libraryRepoMock.Verify(repo => repo.BorrowBook(1, 1), Times.Once);
    }

    [Fact]
    public void ReturnBook_ValidInputs_ShouldCallReturnBookOnLibraryRepo()
    {
        List<Book> books = new List<Book> { new Book { Id = 1, IsBorrowed = true, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        _libraryService.ReturnBook(1);
        _libraryRepoMock.Verify(repo => repo.ReturnBook(1), Times.Once);
    }
    [Fact]
    public void GetBorrowed_shouldreturnonlyborrowedbooks()
    {
        List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                Author = "Author 1",
                IsBorrowed = true,
                BorrowedDate = DateTime.Now,
                BorrowedBy = 1
            },
            new Book
            {
                Id = 2,
                Title = "Book 2",
                Author = "Author 2",
                IsBorrowed = true,
                BorrowedDate = DateTime.Now,
                BorrowedBy = 2
            }
        };

        _libraryRepoMock.Setup(repo => repo.GetBorrowed()).Returns(books);

        List<BookDTO> result = _libraryService.GetBorrowed();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, dto =>
        {
            Assert.True(dto.IsBorrowed);
        });

        _libraryRepoMock.Verify(repo => repo.GetBorrowed(), Times.Once);
    }
}
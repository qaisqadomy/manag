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
    public void BorrowBook_BookNotFound_ShouldThrowNotFoundException()
    {
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(new List<Book>());
        Assert.Throws<NotFound>(() => _libraryService.BorrowBook(1, 1));
    }

    [Fact]
    public void BorrowBook_MemberNotFound_ShouldThrowNotFoundException()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        _memberRepoMock.Setup(repo => repo.GetAll())
            .Returns(new List<Member>());
        Assert.Throws<NotFound>(() => _libraryService.BorrowBook(1, 1));
    }

    [Fact]
    public void BorrowBook_BookDoesntExist_ShouldThrowNotFoundException()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        var members = new List<Member> { new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" } };
        _memberRepoMock.Setup(repo => repo.GetAll()).Returns(members);


        Assert.Throws<NotFound>(() => _libraryService.BorrowBook(2, 1));
    }

    [Fact]
    public void BorrowBook_MemberDoesntExist_ShouldThrowNotFoundException()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        var members = new List<Member> { new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" } };
        _memberRepoMock.Setup(repo => repo.GetAll()).Returns(members);

        Assert.Throws<NotFound>(() => _libraryService.BorrowBook(1, 2));
    }

    [Fact]
    public void BorrowBook_BookAlreadyBorrowed_ShouldThrowInvalidOperationException()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = true, Title = "qais", Author = "qais" } };
        var members = new List<Member> { new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" } };

        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);

        _memberRepoMock.Setup(repo => repo.GetAll())
            .Returns(members);


        Assert.Throws<InvalidOperation>(() => _libraryService.BorrowBook(1, 1));
    }

    [Fact]
    public void BorrowBook_ValidInputs_ShouldCallBorrowBookOnLibraryRepo()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        var members = new List<Member> { new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        _memberRepoMock.Setup(repo => repo.GetAll())
            .Returns(members);
        _libraryService.BorrowBook(1, 1);
        _libraryRepoMock.Verify(repo => repo.BorrowBook(1, 1), Times.Once);
    }


    [Fact]
    public void ReturnBook_BookNotFound_ShouldThrowNotFoundException()
    {
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(new List<Book>());
        Assert.Throws<NotFound>(() => _libraryService.ReturnBook(1));
    }

    [Fact]
    public void ReturnBook_BookDoesntExist_ShouldThrowNotFoundException()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, IsBorrowed = true, Title = "qais", Author = "qais" },
            new Book { Id = 2, IsBorrowed = true, Title = "qais", Author = "qais" }
        };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);

        Assert.Throws<NotFound>(() => _libraryService.ReturnBook(3));
    }

    [Fact]
    public void ReturnBook_BookNotBorrowed_ShouldThrowInvalidOperationException()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = false, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        Assert.Throws<InvalidOperation>(() => _libraryService.ReturnBook(1));
    }

    [Fact]
    public void ReturnBook_ValidInputs_ShouldCallReturnBookOnLibraryRepo()
    {
        var books = new List<Book> { new Book { Id = 1, IsBorrowed = true, Title = "qais", Author = "qais" } };
        _bookRepoMock.Setup(repo => repo.GetAll())
            .Returns(books);
        _libraryService.ReturnBook(1);
        _libraryRepoMock.Verify(repo => repo.ReturnBook(1), Times.Once);
    }
}
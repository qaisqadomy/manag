using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;
using Moq;

namespace TestProject1;

public class BookServiceTests
{
    private readonly Mock<IBookRepo> _mockRepo;
    private readonly BookService _service;

    public BookServiceTests()
    {
        _mockRepo = new Mock<IBookRepo>();
        _service = new BookService(_mockRepo.Object);
    }

    [Fact]
    public void Add_ShouldCallRepoAddOnce()
    {
        var bookDto = new BookDto
        {
            Title = "Book Title",
            Author = "Author Name",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };
        _service.Add(bookDto);
        
        _mockRepo.Verify(r => r.Add(It.Is<Book>(b =>
            b.Title == bookDto.Title &&
            b.Author == bookDto.Author &&
            b.IsBorrowed == bookDto.IsBorrowed &&
            b.BorrowedDate == bookDto.BorrowedDate &&
            b.BorrowedBy == bookDto.BorrowedBy)), Times.Once);
    }

    [Fact]
    public void GetAll_ShouldReturnListOfBookDto()
    {
        var books = new List<Book>
        {
            new Book
            {
                Id = 1, Title = "Book 1", Author = "Author 1", IsBorrowed = false, BorrowedDate = null,
                BorrowedBy = null
            },
            new Book
            {
                Id = 2, Title = "Book 2", Author = "Author 2", IsBorrowed = true,
                BorrowedDate = new DateTime(2024, 1, 1), BorrowedBy = 0
            }
        };

        _mockRepo.Setup(r => r.GetAll()).Returns(books);


        var result = _service.GetAll();


        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.Id == 1 && b.Title == "Book 1" && b.Author == "Author 1");
        Assert.Contains(result, b => b.Id == 2 && b.Title == "Book 2" && b.Author == "Author 2");
    }

    [Fact]
    public void GetAll_ShouldReturnEmptyListOfBookDto()
    {

        _mockRepo.Setup(r => r.GetAll()).Returns(new List<Book>());
        
        var result = _service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void Remove_ShouldCallRepoRemoveOnce()
    {
        int id = 1;

        _service.Remove(id);


        _mockRepo.Verify(r => r.Remove(id), Times.Once);
    }

    [Fact]
    public void Update_ShouldCallRepoUpdateOnce()
    {
        var bookDto = new BookDto
        {
            Id = 1,
            Title = "Updated Title",
            Author = "Updated Author",
        };

        _service.Update(bookDto);

        _mockRepo.Verify(r => r.Update(It.Is<Book>(b =>
            b.Title == bookDto.Title &&
            b.Author == bookDto.Author )), Times.Once);
    }

    [Fact]
    public void Find_ShouldReturnBookDto_WhenBookExists()
    {
        var book = new Book
        {
            Title = "Book Title",
            Author = "Author Name",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };

        _mockRepo.Setup(r => r.Find(1)).Returns(book);
        
        var result = _service.Find(1);
        
        Assert.NotNull(result);
        Assert.Equal("Book Title", result.Title);
        Assert.Equal("Author Name", result.Author);
        Assert.False(result.IsBorrowed);
        Assert.Null(result.BorrowedDate);
        Assert.Null(result.BorrowedBy);
    }

    [Fact]
    public void Find_ShouldThrowNotFoundException_WhenBookDoesNotExist()
    {
        _mockRepo.Setup(r => r.Find(1)).Returns((Book)null!);

        Assert.Throws<NotFound>(() => _service.Find(1));
    }
}
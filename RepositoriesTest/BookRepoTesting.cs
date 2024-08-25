using Application.Repos;
using Domain.Entities;
using Domain.Exeptions;

namespace RepositoriesTest;

public class BookRepoTests
{
    private readonly InMemoryDb _inMemoryDb;
    private readonly BookRepo _bookRepo;

    public BookRepoTests()
    {
        _inMemoryDb = new InMemoryDb();
        _bookRepo = new BookRepo(_inMemoryDb.DbContext);
    }


    [Fact]
    public void Add_ShouldAddBook_WhenBookDoesNotExist()
    {
        Book newBook = TestData.NewBook;

        _bookRepo.Add(newBook);

        Book addedBook = _inMemoryDb.DbContext.Books
            .SingleOrDefault(b => b.Title == newBook.Title && b.Author == newBook.Author)!;

        Assert.NotNull(addedBook);
        Assert.Equal(newBook.Title, addedBook.Title);
        Assert.Equal(newBook.Author, addedBook.Author);
        Assert.False(addedBook.IsBorrowed);
        Assert.Null(addedBook.BorrowedDate);
        Assert.Null(addedBook.BorrowedBy);
    }

    [Fact]
    public void Add_ShouldThrowBookExists_WhenBookExists()
    {
        Book newBook = TestData.NewBook;

        _inMemoryDb.DbContext.Books.Add(newBook);
        _inMemoryDb.DbContext.SaveChanges();
        
        Assert.Throws<BookExists>(() => _bookRepo.Add(newBook));
    }


    [Fact]
    public void Remove_ShouldRemoveBook_WhenBookExists()
    {
        Book existingBook = TestData.NewBook;
        _inMemoryDb.DbContext.Books.Add(existingBook);
        _inMemoryDb.DbContext.SaveChanges();

        _bookRepo.Remove(1);

        Book removedBook = _inMemoryDb.DbContext.Books.SingleOrDefault(b => b.Id == 1)!;
        Assert.Null(removedBook);
    }

    [Fact]
    public void Remove_ShouldThrowBookDoesNotFound_WhenBookDoesNotExist()
    {
        Assert.Throws<BookNotFound>(() => _bookRepo.Remove(5));
    }

    [Fact]
    public void Update_ShouldThrowNotFound_WhenBookDoesNotExist()
    {
        Book updatedBook = TestData.UpdatedBook;

        Assert.Throws<BookNotFound>(() => _bookRepo.Update(updatedBook));
    }

    [Fact]
    public void Update_ShouldUpdateBook_WhenBookExists()
    {
        Book existingBook = TestData.NewBook;
        _inMemoryDb.DbContext.Books.Add(existingBook);
        _inMemoryDb.DbContext.SaveChanges();

        Book updatedBook = TestData.ExistingBook;
        _bookRepo.Update(updatedBook);

        Book book = _inMemoryDb.DbContext.Books.SingleOrDefault(b => b.Id == 1)!;
        Assert.NotNull(book);
        Assert.Equal("Book", book.Title);
        Assert.Equal("Author", book.Author);
    }

    [Fact]
    public void Find_ShouldThrowNotFound_WhenBookDoesNotExist()
    {
        Assert.Throws<BookNotFound>(() => _bookRepo.Find(1));
    }

    [Fact]
    public void Find_ShouldReturnBook_WhenBookExists()
    {
        Book existingBook = TestData.ExistingBook;
        _inMemoryDb.DbContext.Books.Add(existingBook);
        _inMemoryDb.DbContext.SaveChanges();


        Book result = _bookRepo.Find(1);


        Assert.Equal(existingBook, result);
        _inMemoryDb.Dispose();
    }

    [Fact]
    public void GetAll_ShouldReturnAllBooks()
    {
        List<Book> books = TestData.MultipleBooks;

        _inMemoryDb.DbContext.Books.AddRange(books);

        _inMemoryDb.DbContext.SaveChanges();

        List<Book> result = _bookRepo.GetAll();
        Assert.NotNull(result);
        Assert.Equal(books, result);
    }


    [Fact]
    public void IsBorrowed_ShouldReturnTrue_WhenBookIsBorrowed()
    {
        Book borrowedBook = TestData.BorrowedBook;
        _inMemoryDb.DbContext.Books.Add(borrowedBook);
        _inMemoryDb.DbContext.SaveChanges();

        bool result = _bookRepo.IsBorrowed(1);

        Assert.True(result);
    }

    [Fact]
    public void IsBorrowed_ShouldthrowNotFount_WhenBookIsNotFound()
    {
        Assert.Throws<BookNotFound>(() => _bookRepo.IsBorrowed(1));
    }

    [Fact]
    public void IsBorrowed_ShouldReturnFalse_WhenBookIsNotBorrowed()
    {
        Book notBorrowedBook = TestData.NewBook;

        _inMemoryDb.DbContext.Books.Add(notBorrowedBook);
        _inMemoryDb.DbContext.SaveChanges();
        bool result = _bookRepo.IsBorrowed(1);

        Assert.False(result);
    }

    [Fact]
    public void Borrow_ShouldSetBookAsBorrowed()
    {
        Book book = TestData.NewBook;
        int borrowerId = 1;
        _inMemoryDb.DbContext.Books.Add(book);
        _inMemoryDb.DbContext.SaveChanges();
        _bookRepo.Borrow(book, borrowerId);

        Assert.True(book.IsBorrowed);
        Assert.Equal(borrowerId, book.BorrowedBy);
        Assert.NotNull(book.BorrowedDate);
    }

    [Fact]
    public void Borrow_ShouldThrowalreadyBorrowed_WhenBookIsBorrowed()
    {
        const int memberId = 1;
        Book book = TestData.BorrowedBook;
        _inMemoryDb.DbContext.Books.Add(book);
        _inMemoryDb.DbContext.SaveChanges();
        Assert.Throws<AlreadyBorrowed>(() => _bookRepo.Borrow(book, memberId));
    }

    [Fact]
    public void Return_ShouldSetBookAsNotBorrowed()
    {
        Book book = TestData.BorrowedBook;
        _inMemoryDb.DbContext.Books.Add(book);
        _inMemoryDb.DbContext.SaveChanges();
        _bookRepo.Return(book);

        Assert.False(book.IsBorrowed);
        Assert.Null(book.BorrowedBy);
        Assert.Null(book.BorrowedDate);
    }

    [Fact]
    public void Return_ShouldThrowNotBorrowed_WhenBookIsNotBorrowed()
    {
        Book book = TestData.NewBook;
        _inMemoryDb.DbContext.Books.Add(book);
        _inMemoryDb.DbContext.SaveChanges();
        Assert.Throws<NotBorrowed>(() => _bookRepo.Return(book));
    }
}
using Application.Repos;
using Domain.Entities;
using Domain.Exeptions;
using Domain.IRepo;

namespace RepositoriesTest
{
    public class LibraryRepoTests 
    {
        private readonly InMemoryDb _inMemoryDb;
        private readonly LibraryRepo _libraryRepo;

        public LibraryRepoTests()
        {
            _inMemoryDb = new InMemoryDb();
            IBookRepo bookRepo = new BookRepo(_inMemoryDb.DbContext);
            _libraryRepo = new LibraryRepo(bookRepo);
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenBooksCannotBeLoaded()
        {
            Assert.Throws<BookNotFound>(() => _libraryRepo.BorrowBook(1, 1));
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenBookNotFound()
        {
            _inMemoryDb.DbContext.Books.RemoveRange(_inMemoryDb.DbContext.Books);
            _inMemoryDb.DbContext.SaveChanges();


            Assert.Throws<BookNotFound>(() => _libraryRepo.BorrowBook(1, 1));
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenBookIsAlreadyBorrowed()
        {
            var books = new List<Book> { TestData.BorrowedBook };
            _inMemoryDb.DbContext.Books.AddRange(books);
            _inMemoryDb.DbContext.SaveChanges();


            Assert.Throws<AlreadyBorrowed>(() => _libraryRepo.BorrowBook(1, 1));
        }

        [Fact]
        public void BorrowBook_ShouldBorrowBook_WhenValid()
        {
            var books = new List<Book>{TestData.NotBorrowedBook };
            _inMemoryDb.DbContext.Books.AddRange(books);
            _inMemoryDb.DbContext.SaveChanges();


            _libraryRepo.BorrowBook(1, 1);


            var book = _inMemoryDb.DbContext.Books.SingleOrDefault(b => b.Id == 1);
            Assert.NotNull(book);
            Assert.True(book.IsBorrowed);
            Assert.Equal(1, book.BorrowedBy);
        }

        [Fact]
        public void ReturnBook_ShouldThrowException_WhenBooksCannotBeLoaded()
        {
            Assert.Throws<BookNotFound>(() => _libraryRepo.ReturnBook(1));
        }

        [Fact]
        public void ReturnBook_ShouldThrowException_WhenBookNotFound()
        {
            _inMemoryDb.DbContext.Books.RemoveRange(_inMemoryDb.DbContext.Books);
            _inMemoryDb.DbContext.SaveChanges();


            Assert.Throws<BookNotFound>(() => _libraryRepo.ReturnBook(1));
        }

        [Fact]
        public void ReturnBook_ShouldThrowException_WhenBookIsNotBorrowed()
        {
            var books = new List<Book>{TestData.ExistingBook };
            _inMemoryDb.DbContext.Books.AddRange(books);
            _inMemoryDb.DbContext.SaveChanges();


            Assert.Throws<NotBorrowed>(() => _libraryRepo.ReturnBook(1));
        }

        [Fact]
        public void ReturnBook_ShouldReturnBook_WhenValid()
        {
            List<Book> books = new List<Book>
            {TestData.BorrowedBook
            };
            _inMemoryDb.DbContext.Books.AddRange(books);
            _inMemoryDb.DbContext.SaveChanges();


            _libraryRepo.ReturnBook(1);

            Book book = _inMemoryDb.DbContext.Books.SingleOrDefault(b => b.Id == 1)!;
            Assert.NotNull(book);
            Assert.False(book.IsBorrowed);
            Assert.Null(book.BorrowedBy);
            Assert.Null(book.BorrowedDate);
        }
        [Fact]
        public void GetBorrowed_shouldreturnonlyborrowedbooks(){
            List<Book> books =  TestData.MultipleborroedBooks;
              _inMemoryDb.DbContext.Books.AddRange(books);
            _inMemoryDb.DbContext.SaveChanges();
            List<Book> fromdata = _libraryRepo.GetBorrowed();
            Assert.Equal(books,fromdata);
        }
    }
}
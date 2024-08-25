using Domain.Entities;

namespace RepositoriesTest;

public static class TestData
{
    public static Member ExistingMember => new Member { Name = "qais", Email = "qais@gmail.com" };


    public static Member CreateNewMember => new Member { Name = "qais", Email = "qais@gmail.com" };
    public static Member UpdatedMember => new Member {Id = 1,Name = "ahmad", Email = "ahmad@gmail.com" };


    public static List<Member> CreateMultipleMembers =>
    [
        new Member { Id = 1, Name = "qais", Email = "qais@gmail.com" },
        new Member { Id = 2, Name = "ahmad", Email = "ahmad@gmail.com" }
    ];
    
    
    ///////////////////////book
     
    public static Book ExistingBook => new Book
        {
            Id = 1,
            Title = "Book",
            Author = "Author",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };
    

    public static Book NewBook => new Book
        {
            Title = "New Book",
            Author = "new Author",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };
    public static Book UpdatedBook => new Book
    {
        Title = "Updated Book",
        Author = "Updated Author",
    };

    public static List<Book> MultipleBooks =>
        [
            new Book
            {
                Id = 1,
                Title = "Book 1",
                Author = "Author 1",
                IsBorrowed = false,
                BorrowedDate = null,
                BorrowedBy = null
            },

            new Book
            {
                Id = 2,
                Title = "Book 2",
                Author = "Author 2",
                IsBorrowed = false,
                BorrowedDate = null,
                BorrowedBy = null
            }
        ];
        public static List<Book> MultipleborroedBooks =>
        [
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
        ];
    

    public static Book BorrowedBook =>new Book
        {
            Title = "Borrowed Book",
            Author = "Author",
            IsBorrowed = true,
            BorrowedDate = DateTime.Now,
            BorrowedBy = 1
        };
    

    public static Book NotBorrowedBook => new Book
        {
            Title = "Not Borrowed Book",
            Author = "Author",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };
}
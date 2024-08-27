namespace Application.DTOs;

public class BookDtoCreate
{
        public required string Title { get; set; }
        public required string Author { get; set; }
        public bool IsBorrowed { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public int? BorrowedBy { get; set; }
}

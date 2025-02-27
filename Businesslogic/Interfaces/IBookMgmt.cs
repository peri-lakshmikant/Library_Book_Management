using Entities.Domain;
using Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBookMgmt
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> PopularBooks();
        Book GetBookById(int id);
        string AddBook(AddBookReq book);
        string UpdateBook(UpdateBookReq updatedBook);
        string DeleteBook(int id);
        IEnumerable<Book> SearchBooks(SearchBookReq search);
        string BorrowBook(int id, int memberId);
        string ReturnBook(int id, bool finesWaived);
        string HoldBook(int id, int memberId);


    }
}

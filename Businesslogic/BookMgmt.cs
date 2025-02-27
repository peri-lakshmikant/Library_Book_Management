using Businesslogic.Data;
using BusinessLogic.Interfaces;
using Entities.Domain;
using Entities.Request;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslogic
{
   public class BookMgmt : IBookMgmt
    {
        public static int DueDays = 14;
        public static int FinePerDay = 1;
        public static int holdFor = 3;

        public IEnumerable<Book> GetAllBooks() {
             return BooksList.Books;
        }

        public Book GetBookById(int id) 
        { 
            var book = BooksList.Books.FirstOrDefault(b => b.Id == id); 
            return book;            
        }

        public IEnumerable<Book> PopularBooks()
        {
            var books = BooksList.Books.OrderByDescending(b => b.BorrowedFor).Take(5).ToList();
            return books;
        }


        public string AddBook(AddBookReq addBook)
        {
                string msg = "";
            if (string.IsNullOrEmpty(addBook.Title) ||
                string.IsNullOrEmpty(addBook.Author) ||
                string.IsNullOrEmpty(addBook.ISBN) ||
                string.IsNullOrEmpty(addBook.Genere) ||
                string.IsNullOrEmpty(addBook.PublishedYear))
            {
                msg = "Please make sure Title/Author/ISBN/Genere and PublishedYear are provided";
            }
            else
            {
                Book book = new Book();
                book.Id = BooksList.Books.Max(b => b.Id) + 1;
                book.Title = addBook.Title;
                book.Author = addBook.Author;
                book.ISBN = addBook.ISBN;
                book.Genere = addBook.Genere;
                book.PublishedYear = addBook.PublishedYear;
                book.AvailableDate = DateTime.Now;
                book.BorrowedDate = DateTime.Now;
                book.BorrowedFor = 0;
                book.MemberID = 0;
                book.FineWaived = false;
                book.Available = true;
                book.OnHold = false;

                
                BooksList.Books.Add(book);
                msg = "Book " + book.Title + " has been successfully added";
            }
            return msg;
        }
        public string UpdateBook(UpdateBookReq updatedBook)
        {
            string msg = "";
            var book = BooksList.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.ISBN = updatedBook.ISBN;
                book.PublishedYear = updatedBook.PublishedYear;
                book.Available = updatedBook.Available;

                msg = "Book details has been updated successfully";
            }
            else
            {
                msg = "The book that you are trying to update is not available in the library";
            }
            return msg;
        }

        public string DeleteBook(int id)
        {
            string msg = "";
            var book = BooksList.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {

                BooksList.Books.RemoveAll(b => (b.Id == id && b.Available == true));
                msg = "The specific book has been deleted successfully";
            }
            else
            {
                msg = "The book that you are trying to delete is not available in the library";
            }
            return msg;
        }

        public IEnumerable<Book>? SearchBooks(SearchBookReq search)
        {
            var foundBook = BooksList.Books.FindAll(b => (b.Title.Contains(search.Title, StringComparison.OrdinalIgnoreCase)) || // Search by Title
                (b.Author.Contains(search.Author, StringComparison.OrdinalIgnoreCase)) || //Search by Author
                (b.ISBN.Contains(search.ISBN, StringComparison.OrdinalIgnoreCase)) ||  // Search by ISBN
                (b.Genere.Contains(search.Genere, StringComparison.OrdinalIgnoreCase)) || // Search by Genere
                (b.PublishedYear.Contains(search.PublishedYear, StringComparison.OrdinalIgnoreCase)) // Search by PublisedYear
            );
            if(foundBook != null)
            {
                return foundBook;
            }

            return null;
            
        }

        public string BorrowBook(int id, int memberId)
        {
            var book = BooksList.Books.FirstOrDefault(b => b.Id == id);
            string msg = "";
            if (book != null)
            {
                if ((book.OnHold == false) || (book.OnHold == true && DateTime.Now > book.AvailableDate))
                {
                    book.Available = false;
                    book.AvailableDate = DateTime.Now.AddDays(DueDays).Date;
                    book.BorrowedDate = DateTime.Now;
                    book.BorrowedFor = book.BorrowedFor + 1;
                    book.MemberID = memberId;
                    book.OnHold = false;

                    msg = "You have successfully borrowed the book";
                }
                else
                {
                    msg = "The requested book is on hold, so you cannot borrow until " + book.AvailableDate.ToShortDateString();
                }
            }
            else
            {
                msg = "The requested book is not available";
            }
            return msg;

        }

        public string ReturnBook(int id,bool finesWaived)
        {
            var book = BooksList.Books.FirstOrDefault(b => b.Id == id);
            var fines = 0.0;
            string msg = "";
            if (book != null)
            {
                if (book.BorrowedDate.AddDays(DueDays).Date < DateTime.Now.Date)
                {
                    if (!finesWaived)
                    {
                        fines = (DueDays - (book.BorrowedDate - DateTime.Now).TotalDays) * FinePerDay;
                    }
                }
                book.Available = true;
                book.BorrowedDate = DateTime.Now;
                book.MemberID = 0;

                msg = "Book Successfully returned and you have a fine of $" + fines.ToString();
            }
            else
            {
                msg = "This book doesnt belong to this library";
            }
            return msg;
        }


        public string HoldBook(int id, int memberId)
        {
            string msg = "";
            if (id == 0 && memberId == 0)
            {
                return "Please provide the Book ID and the MemberID for the book that you are trying to put on hold";
            }
            var book = BooksList.Books.FirstOrDefault(b => b.Id == id);
            if (book != null && book.Available)
            {
                book.OnHold = true;
                book.Available = true;
                book.AvailableDate = DateTime.Now.AddDays(holdFor);
                book.BorrowedDate = DateTime.Now;
                book.MemberID = memberId;

                msg = "Book has been put on hold for Memeber ID - " + memberId + " for 3 days";
            }
            else
            {
                msg = "Sorry, the book you are trying to put on hold is not found in the library";
            }
            return msg;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Businesslogic.Data;
using BusinessLogic.Interfaces;
using Entities.Domain;
using System.ComponentModel.DataAnnotations;
using Entities.Request;


namespace Library_Book_Management.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BookMgmtController : Controller
    {
        private readonly IBookMgmt _bookMgmt;

        public BookMgmtController(IBookMgmt bookMgmt)
        {
            _bookMgmt = bookMgmt;
        }
        
        [Route("All")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_bookMgmt.GetAllBooks());
        }

        [Route("ById")]
        [HttpGet]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookMgmt.GetBookById(id);
            if (book == null)
            {
                return NotFound("The book with ID - "+id+" was not found in the system");
            }
            return Ok(book);
        }

        [Route("Popular")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> PopularBooks()
        {
            return Ok(_bookMgmt.PopularBooks());
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult<string> AddBook(AddBookReq book)
        {
            string msg = _bookMgmt.AddBook(book);
            return Ok(msg);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult<string> UpdateBook(UpdateBookReq book)
        {
            if(book.Id == 0)
            {
                return BadRequest("The book Id cannot be 0");
            }
            string msg = _bookMgmt.UpdateBook(book);
            return Ok(msg);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult<string> DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest("The book Id cannot be 0");
            }
            string msg = _bookMgmt.DeleteBook(id);
            return Ok(msg);
        }

        [Route("Search")]
        [HttpPost]
        public ActionResult<IEnumerable<Book>> SearchBooks(SearchBookReq search)
        {
            var existingBook = _bookMgmt.SearchBooks(search);
            if (existingBook == null)
            {
                return NotFound("Sorry, we dont have the book that you are looking for");
            }

            return Ok(existingBook);
        }

        [Route("Borrow")]
        [HttpPost]
        public ActionResult<string> BorrowBook(int id, int memberId)
        {
            var msg = "";
            msg = _bookMgmt.BorrowBook(id, memberId);
            return Ok(msg);
        }

        [Route("Return")]
        [HttpPost]
        public ActionResult<string> ReturnBook(int id, bool finesWaived)
        {
            var msg = "";
            msg = _bookMgmt.ReturnBook(id,finesWaived);    
            return Ok(msg);
        }

        [Route("Hold")]
        [HttpPost]
        public ActionResult<string> HoldBook(int id, int memberId)
        {
            var msg = "";
            msg = _bookMgmt.HoldBook(id, memberId);            
            return Ok(msg);
        }
    }
}

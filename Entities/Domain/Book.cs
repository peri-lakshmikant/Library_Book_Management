using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genere { get; set; }
        public string PublishedYear { get; set; }
        public bool Available { get; set; }
        public DateTime AvailableDate { get; set; }
        public DateTime BorrowedDate { get; set; }
        public int MemberID { get; set; }
        public bool FineWaived { get; set; }
        public bool OnHold { get; set; }
        public long BorrowedFor { get; set; }

    }
}

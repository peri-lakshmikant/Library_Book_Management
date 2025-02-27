using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Request
{
    public class UpdateBookReq
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genere { get; set; }
        public string PublishedYear { get; set; }
        public bool Available { get; set; }     
    }
}


using Aplikacja_Desktopowa2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_Desktopowa2.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
